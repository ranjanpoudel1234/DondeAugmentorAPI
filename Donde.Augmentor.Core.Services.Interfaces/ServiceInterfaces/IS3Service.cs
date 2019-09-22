using System.Threading.Tasks;
using Donde.Augmentor.Core.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IS3Service
    {
        Task<S3Response> CreateBucketAsync(string bucketName);
        Task GetObjectFromS3Async(string bucketName);
        Task<Audio> UploadObjectAsync(IFormFile file, string bucketName);
    }
}
