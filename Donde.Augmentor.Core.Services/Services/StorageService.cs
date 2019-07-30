using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;

namespace Donde.Augmentor.Core.Services.Services
{
    public class StorageService
    {
        private IAmazonS3 client = null;

        public StorageService()
        {
            //string accessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            //string secretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            //if (this.client == null)
            //{
            //    this.client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey, RegionEndpoint.APSoutheast1);
            //}
        }

        public bool UploadFile(string awsBucketName, string key, Stream stream)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                BucketName = awsBucketName,
                CannedACL = S3CannedACL.AuthenticatedRead,
                Key = key,
               
            };

            TransferUtility fileTransferUtility = new TransferUtility(this.client);
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

            urlString = this.client.GetPreSignedURL(request);
            return urlString;
        }
    }
}
