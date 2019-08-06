using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private const string ServerTempUploadFolderName = "TempUploads"; //todo make these configurable
        private const string BucketName = "donde-augmentor-dev-bucket";
        private const string ImageFolderName = "DondeAugmentorImages";
        private const string VideosFolderName = "DondeAugmentorVideos";



        public FileProcessingService(IHostingEnvironment hostingEnvironment, IFileStreamContentReaderService fileStreamContentReaderService, IStorageService storageService)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileStreamContentReaderService = fileStreamContentReaderService;
            _storageService = storageService;
        }

        public async Task<bool> UploadImageAsync(HttpRequest request)
        {
            var fileStreamReadResponse = await _fileStreamContentReaderService.StreamFileAsync(request);

            if(fileStreamReadResponse)
            {
                using (var stream = _fileStreamContentReaderService.Stream)
                {
                    var fileExtension = Path.GetExtension(_fileStreamContentReaderService.FileName);

                    var uniqueFileGuid = Guid.NewGuid().ToString();
                    var uniqueFileName = Path.ChangeExtension(uniqueFileGuid, fileExtension);
                    var filePath = await CreateFileLocallyAndReturnPathAsync(stream, uniqueFileName);

                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        if (!ResizeImage(filePath))
                            return false;

                        var uploadResult = await _storageService.UploadFileAsync(BucketName, $"{ImageFolderName}/{uniqueFileName}", filePath);

                        if (!uploadResult)
                            return false; //todo add better error handlings in all of this

                        DeleteFileFromPath(filePath);

                        //todo call AugmentImageService here to pass in the metadata and the uploadId.
                    }
                }
            }

            return true;        
        }

        private bool ResizeImage(string filePath)
        {
            using (Image<Rgba32> image = Image.Load(filePath))
            {
                image.Mutate(x => x
                     .Resize(new ResizeOptions
                     {
                         Size = new Size(200, 200), //todo potentially make these configurable along with quality and size.
                         Mode = ResizeMode.Stretch
                     })

                     );
                var encoder = new JpegEncoder();
                encoder.Quality = 75;
                encoder.Encode(image, new MemoryStream());

                image.Save(filePath, encoder); // Automatic encoder selected based on extension.
                return true;
            }
        }

        private async Task<string> CreateFileLocallyAndReturnPathAsync(Stream stream, string fileName)
        {
            var filePath = string.Empty;
            var contentRoot = _hostingEnvironment.ContentRootPath;
            var uploadsPath = Path.Combine(contentRoot, ServerTempUploadFolderName);
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
