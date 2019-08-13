using CSharpFunctionalExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
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
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileStreamContentReaderService _fileStreamContentReaderService;
        private readonly IStorageService _storageService;
        private readonly ILogger<FileProcessingService> _logger;
        private readonly DomainSettings _domainSettings;

        public FileProcessingService(IHostingEnvironment hostingEnvironment,
            IFileStreamContentReaderService fileStreamContentReaderService,
            IStorageService storageService,
            DomainSettings domainSettings,
            ILogger<FileProcessingService> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileStreamContentReaderService = fileStreamContentReaderService;
            _storageService = storageService;
            _domainSettings = domainSettings;
            _logger = logger;
        }

        public async Task<Result<MediaAttachmentDto>> UploadImageAsync(HttpRequest request)
        {
            var fileStreamReadResponse = await _fileStreamContentReaderService.StreamFileAsync(request);

            if (fileStreamReadResponse)
            {
                //TODO validate the stream fileName and extension here to be same as what ViroMedia supports

                var uploadFileResult = await UploadFileAsync();
                if (uploadFileResult.IsFailure)
                {
                    Result.Fail<MediaAttachmentDto>("Failure in uploading image");
                }
            }

            return Result.Fail<MediaAttachmentDto>("Empty File Stream");
        }


        public async Task<Result<MediaAttachmentDto>> UploadVideoAsync(HttpRequest request)
        {
            var fileStreamReadResponse = await _fileStreamContentReaderService.StreamFileAsync(request);

            if (fileStreamReadResponse)
            {
                //TODO validate the stream fileName and extension here to be same as what ViroMedia supports

                var uploadFileResult = await UploadFileAsync();
                if (uploadFileResult.IsFailure)
                {
                    Result.Fail<MediaAttachmentDto>("Failure in uploading image");
                }
            }

            return Result.Fail<MediaAttachmentDto>("Empty File Stream");
        }

        private async Task<Result<MediaAttachmentDto>> UploadFileAsync()
        {
            using (var stream = _fileStreamContentReaderService.Stream)
            {
                var fileExtension = Path.GetExtension(_fileStreamContentReaderService.FileName);

                var uniqueFileGuid = Guid.NewGuid();
                var uniqueFileName = Path.ChangeExtension(uniqueFileGuid.ToString(), fileExtension);
                var filePath = await CreateFileLocallyAndReturnPathAsync(stream, uniqueFileName);

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    if (!ResizeImage(filePath))
                    {
                        return Result.Fail<MediaAttachmentDto>("Could not resize file");
                    }

                    var uploadResult = await _storageService.UploadFileAsync(_domainSettings.UploadSettings.BucketName, $"{_domainSettings.UploadSettings.ImageFolderName}/{uniqueFileName}", filePath);

                    if (uploadResult.IsFailure)
                    {
                        _logger.LogError("Failure in storage service");
                        return Result.Fail<MediaAttachmentDto>("Failure in storage service while uploading file");
                    }

                    DeleteFileFromPath(filePath);

                    var attachmentDto = new MediaAttachmentDto()
                    {
                        Id = uniqueFileGuid,
                        FileName = _fileStreamContentReaderService.FileName,
                        FilePath = filePath,
                        MimeType = _fileStreamContentReaderService.MimeType
                    };

                    return Result.Ok(attachmentDto);
                }
            }

            return Result.Fail<MediaAttachmentDto>("Error in uploading file");
        }

        //private async bool ValidateImageAsync()
        //{
        //    return false;
        //}

        //private async bool ValidateVideoAsync()
        //{
        //    return null;
        //}

        private bool ResizeImage(string filePath)
        {
            using (Image<Rgba32> image = Image.Load(filePath))
            {
                image.Mutate(x => x
                     .Resize(new ResizeOptions
                     {
                         Size = new Size(_domainSettings.MediaSettings.ImageResizeHeight, _domainSettings.MediaSettings.ImageResizeWidth), //todo potentially make these configurable along with quality and size.
                         Mode = ResizeMode.Stretch
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
            var contentRoot = _hostingEnvironment.ContentRootPath;
            var uploadsPath = Path.Combine(contentRoot, _domainSettings.UploadSettings.ServerTempUploadFolderName);
            var directoryExists = Directory.Exists(uploadsPath);

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var path = Path.Combine(uploadsPath, Path.Combine(fileName));

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
                // todo throw some invalid exception here.
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
