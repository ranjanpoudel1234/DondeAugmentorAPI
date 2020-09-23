
using CSharpFunctionalExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Validations;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services.FileService
{
    //TODO add unit test around this 
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileStreamContentReaderService _fileStreamContentReaderService;
        private readonly IStorageService _storageService;
        private readonly ILogger<FileProcessingService> _logger;
        private readonly DomainSettings _domainSettings;
        private readonly IValidator<MediaAttachmentDto> _validator;

        public FileProcessingService(IHostingEnvironment hostingEnvironment,
            IFileStreamContentReaderService fileStreamContentReaderService,
            IStorageService storageService,
            DomainSettings domainSettings,
            ILoggerFactory loggerFactory,
            IValidator<MediaAttachmentDto> validator)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileStreamContentReaderService = fileStreamContentReaderService;
            _storageService = storageService;
            _domainSettings = domainSettings;
            _logger = loggerFactory.CreateLogger<FileProcessingService>();
            _validator = validator;
        }

        public async Task<Result<MediaAttachmentDto>> UploadMediaAsync(HttpRequest request, MediaTypes mediaType, Guid? mediaId = null)
        {
            var fileStreamReadResponse = await _fileStreamContentReaderService.StreamFileAsync(request);

            if(!fileStreamReadResponse)
                return Result.Fail<MediaAttachmentDto>("Empty File Stream");
          
            var uploadFileResult = await ValidateAndUploadFileAsync(mediaType, mediaId);
            if (uploadFileResult.IsFailure)
            {
                Result.Fail<MediaAttachmentDto>($"Failure in uploading {nameof(mediaType)}");
            }

            return uploadFileResult;
        }

        private async Task<Result<MediaAttachmentDto>> ValidateAndUploadFileAsync(MediaTypes mediaType, Guid? mediaId = null)
        {
            _logger.LogError("FileStreamReaderService {@FileStreamContentReaderService}", _fileStreamContentReaderService);
            using (var stream = _fileStreamContentReaderService.Stream)
            {
                var fileExtension = Path.GetExtension(_fileStreamContentReaderService.FileName);

                var uniqueFileGuid = mediaId ?? SequentialGuidGenerator.GenerateComb();
                var uniqueFileName = Path.ChangeExtension(uniqueFileGuid.ToString(), fileExtension);
                var localFilePath = await CreateFileLocallyAndReturnPathAsync(stream, uniqueFileName);

                try
                {
                    if (!string.IsNullOrWhiteSpace(localFilePath))
                    {
                        var attachmentDto = new MediaAttachmentDto()
                        {
                            Id = uniqueFileGuid,
                            FileName = _fileStreamContentReaderService.FileName,
                            MimeType = _fileStreamContentReaderService.MimeType
                        };

                        if (mediaType == MediaTypes.Image || mediaType == MediaTypes.Logo)
                        {
                            attachmentDto.FilePath = mediaType == MediaTypes.Image ? 
                                $"{ _domainSettings.UploadSettings.ImagesFolderName }/{ uniqueFileName}" 
                                : $"{ _domainSettings.UploadSettings.LogosFolderName }/{ uniqueFileName}";

                            await _validator.ValidateOrThrowAsync(attachmentDto, ruleSets: $"{MediaAttachmentDtoValidator.DefaultRuleSet},{MediaAttachmentDtoValidator.ImageFileRuleSet}");

                            var originalUploadResult = await UploadOriginalFileAsync(mediaType, uniqueFileName, localFilePath);
                            if (originalUploadResult.IsFailure)
                            {
                                DeleteFileFromPath(localFilePath);
                                return Result.Fail<MediaAttachmentDto>("Failure in uploading original image");
                            }

                            if (!ResizeImage(localFilePath))
                            {
                                DeleteFileFromPath(localFilePath);
                                return Result.Fail<MediaAttachmentDto>("Could not resize image file");
                            }
                        }
                        else if (mediaType == MediaTypes.Video)
                        {
                            attachmentDto.FilePath = $"{ _domainSettings.UploadSettings.VideosFolderName }/{ uniqueFileName}";
                            await _validator.ValidateOrThrowAsync(attachmentDto, ruleSets: $"{MediaAttachmentDtoValidator.DefaultRuleSet},{MediaAttachmentDtoValidator.VideoFileRuleSet}");
                            //todo potentially convert video here. May be move this to interface with implementation with handler so i dont have to do switch.
                        }
                        else if (mediaType == MediaTypes.Audio)
                        {
                            attachmentDto.FilePath = $"{ _domainSettings.UploadSettings.AudiosFolderName }/{ uniqueFileName}";
                            await _validator.ValidateOrThrowAsync(attachmentDto, ruleSets: $"{MediaAttachmentDtoValidator.DefaultRuleSet},{MediaAttachmentDtoValidator.AudioFileRuleSet}");
                        }

                        var uploadResult = await _storageService.UploadFileAsync
                            (_domainSettings.UploadSettings.BucketName,
                            attachmentDto.FilePath,
                            localFilePath);

                        if (uploadResult.IsFailure)
                        {
                            DeleteFileFromPath(localFilePath);
                            _logger.LogError("Failure in storage service");
                            return Result.Fail<MediaAttachmentDto>("Failure in storage service while uploading file");
                        }

                        DeleteFileFromPath(localFilePath);

                        return Result.Ok(attachmentDto);
                    }
                }
                catch (Exception ex)
                {
                    DeleteFileFromPath(localFilePath); // cleanup
                    throw ex;
                }        
            }

            return Result.Fail<MediaAttachmentDto>("Error in uploading file");
        }

        private async Task<Result<bool>> UploadOriginalFileAsync(MediaTypes mediaType, string uniqueFileName, string localFilePath)
        {
            var originalFileRemotePath = mediaType == MediaTypes.Image ?
                               $"{ _domainSettings.UploadSettings.ImagesFolderName }/{_domainSettings.UploadSettings.OriginalImageSubFolderName}/{ uniqueFileName}"
                               : $"{ _domainSettings.UploadSettings.LogosFolderName }/{_domainSettings.UploadSettings.OriginalImageSubFolderName}/{ uniqueFileName}";

            var uploadResult = await _storageService.UploadFileAsync
                           (_domainSettings.UploadSettings.BucketName,
                           originalFileRemotePath,
                           localFilePath);

            return uploadResult;
        }

        private bool ResizeImage(string filePath)
        {
            using (Image<Rgba32> image = Image.Load(filePath))
            {
                image.Mutate(x => x
                     .Resize(new ResizeOptions
                     {
                         Size = new Size(_domainSettings.MediaSettings.ImageResizeHeight, _domainSettings.MediaSettings.ImageResizeWidth),
                         Mode = ResizeMode.Max
                     })

                     );
                var encoder = new JpegEncoder();
                encoder.Quality = _domainSettings.MediaSettings.ImageQuality;
                encoder.Encode(image, new MemoryStream());

                image.Save(filePath, encoder); // Automatic encoder selected based on extension.
                return true;
            }
        }

        private async Task<string> CreateFileLocallyAndReturnPathAsync(Stream stream, string fileName)
        {
            var filePath = string.Empty;
            var uploadsPath = Directory.GetCurrentDirectory();
            uploadsPath = Path.Combine(uploadsPath, _domainSettings.UploadSettings.ServerTempUploadFolderName);

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var path = Path.Combine(uploadsPath, Path.Combine(fileName));

            _logger.LogError("CreateFileLocallyAndReturnPathAsync: Path {@path}", path);

            try
            {
                if (File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
                }
                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await stream.CopyToAsync(fs);
                    filePath = path;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CreateFileLocallyAndReturnPathAsync: Exception {@Exception}", ex);
                throw ex;
            }

            return filePath;
        }

        private bool DeleteFileFromPath(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return true;
        }
    }
}
