using Amazon.S3;
using Amazon.S3.Transfer;
using CSharpFunctionalExtensions;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class StorageService : IStorageService
    {
        IAmazonS3 _client { get; set; }
        private readonly DomainSettings _domainSettings;
        private readonly ILogger<StorageService> _logger;

        public StorageService(IAmazonS3 client, DomainSettings domainSettings, ILogger<StorageService> logger)
        {
            _client = client;
            _domainSettings = domainSettings;
            _logger = logger;
        }

        public async Task<Result<bool>> UploadFileAsync(string awsBucketName, string key, string filePath)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_client);

                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = awsBucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.Standard,
                    PartSize = _domainSettings.UploadSettings.UploadPartSizeInMB * 1024 * 1024,
                    Key = key,
                    CannedACL = S3CannedACL.PublicRead
                };
             

                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);

                return Result.Ok(true);
            }
            catch (AmazonS3Exception e)
            {
                _logger.LogError($"AmazonS3Exception Occurred during upload {e.Message}");
                return Result.Fail<bool>($"AmazonS3Exception Occurred during upload {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error Occurred during upload {e.Message}");
                return Result.Fail<bool>($"Error Occurred during upload {e.Message}");
            }
        }
    }
}
