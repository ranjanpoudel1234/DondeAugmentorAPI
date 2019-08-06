using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services
{
    public class StorageService : IStorageService
    {
        IAmazonS3 _client { get; set; }
        private const int PartSize = 6291456; // 2 MB


        public StorageService(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<bool> UploadFileAsync(string awsBucketName, string key, string filePath)
        {
            try
            {
                var fileTransferUtility =
                    new TransferUtility(_client);


                // Option 4. Specify advanced settings.
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = awsBucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.Standard,
                    PartSize = PartSize, // 2 MB.
                    Key = key,
                    CannedACL = S3CannedACL.PublicRead
                };
             

                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                return true;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }

            return false;
        }
    }
}
