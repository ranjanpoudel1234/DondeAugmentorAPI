//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Amazon.S3;
//using Amazon.S3.Model;
//using Amazon.S3.Transfer;
//using Amazon.S3.Util;
//using Donde.Augmentor.Core.Domain.Models;
//using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Donde.Augmentor.Core.Services.Services
//{
//    public class S3Service : IS3Service
//    {
//        private readonly IAmazonS3 _client;

//        private const string FilePath = "";
//        private const string UploadWithKeyName = "";
//        private const string FileStreamUpload = "";
//        private const string AdvancedUpload = "";

//        private const string AccessKey = "AKIAJICHYUHYCAON4GBA";
//        private const string AccessSecret = "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";

//        public S3Service(IAmazonS3 client)
//        {
//            _client = client;
//        }

//        public async Task<S3Response> CreateBucketAsync(string bucketName)
//        {
//            try
//            {
//                if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, bucketName))
//                {
//                    var putBucketRequest = new PutBucketRequest
//                    {
//                        BucketName = bucketName,
//                        UseClientRegion = true
//                    };

//                    var response = await _client.PutBucketAsync(putBucketRequest);

//                    return new S3Response
//                    {
//                        Message = response.ResponseMetadata.RequestId,
//                        Status = response.HttpStatusCode
//                    };
//                }
//            }
//            catch (AmazonS3Exception e)
//            {
//                return new S3Response()
//                {
//                    Status = e.StatusCode,
//                    Message = e.Message
//                };
//            }
//            catch (Exception e)
//            {
//                return new S3Response()
//                {
//                    Status = HttpStatusCode.InternalServerError,
//                    Message = e.Message
//                };
//            }

//            return new S3Response
//            {
//                Status = HttpStatusCode.InternalServerError,
//                Message = "Something went wrong"
//            };

//        }

//        public async Task<Audio> UploadObjectAsync(IFormFile file, string bucketName)
//        {
//            // connecting to the client
//            var client = new AmazonS3Client(AccessKey, AccessSecret, Amazon.RegionEndpoint.USEast1);

//            // get the file and convert it to the byte[]
//            byte[] fileBytes = new Byte[file.Length];
//            file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

//            // create unique file name for prevent the mess
//            var fileName = Guid.NewGuid() + file.FileName;

//            PutObjectResponse response = null;

//            using (var stream = new MemoryStream(fileBytes))
//            {
//                var request = new PutObjectRequest
//                {
//                    BucketName = bucketName,
//                    Key = fileName,
//                    InputStream = stream,
//                    ContentType = file.ContentType,
//                    CannedACL = S3CannedACL.PublicRead
//                };

//                try
//                {
//                    response = await client.PutObjectAsync(request);

//                }
//                catch (AmazonS3Exception s3exception)
//                {

//                }

//            };

//            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
//            {
//                return new Audio
//                {
//                    Name = file.FileName
//                };
//            }
//            else
//            {
//                return new Audio
//                {
//                    Name = file.FileName
//                };
//            }
//        }

//        public async Task GetObjectFromS3Async(string bucketName)
//        {
//            const string keyName = "RankingReport.pdf ";

//            try
//            {
//                var request = new GetObjectRequest
//                {
//                    BucketName = bucketName,
//                    Key = keyName
//                };

//                string responseBody;

//                using (var response = await _client.GetObjectAsync(request))
//                using (var responseStream = response.ResponseStream)
//                using (var reader = new StreamReader(responseStream))
//                {
//                    var title = response.Metadata["x-amz-meta-title"];
//                    var contentType = response.Headers["Content-Type"];

//                    Console.WriteLine($"Object meta, Title: {title}");
//                    Console.WriteLine($"ContentType: {contentType}");

//                    responseBody = reader.ReadToEnd();
//                }

//                //saves file in this path
//                var pathAndFileName = $"C:\\Users\\psherchan\\Desktop\\Pankaj\\{keyName}";

//                var createText = responseBody;

//                File.WriteAllText(pathAndFileName, createText);
//            }
//            catch (AmazonS3Exception e)
//            {
//                Console.WriteLine("Error encountered on server. Message: '{0}' when writing an object", e.Message);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error encountered on server. Message: '{0}' when writing an object", ex.Message);
//            }
//        }

//    }
//}
