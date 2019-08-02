using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.IO;

namespace Donde.Augmentor.Core.Services.Services
{
    public class StorageService : IStorageService
    {
        IAmazonS3 _client { get; set; }

        public StorageService(IAmazonS3 client)
        {
            _client = client;
        }

        public bool UploadFile(string awsBucketName, string key, Stream stream)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                BucketName = awsBucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = key,
                PartSize = 5000000
               
            };

            TransferUtility fileTransferUtility = new TransferUtility(_client);
            fileTransferUtility.Upload(uploadRequest);
            return true;
        }

        public string GeneratePreSignedURL(string awsBucketName, string key, int expireInSeconds)
        {
            string urlString = string.Empty;
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = awsBucketName,
                Key = key,
                Expires = DateTime.Now.AddSeconds(expireInSeconds)
            };

            urlString = this._client.GetPreSignedURL(request);
            return urlString;
        }
    }
}
