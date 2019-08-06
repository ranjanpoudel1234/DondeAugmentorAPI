using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IStorageService
    {
        Task<bool> UploadFileAsync(string awsBucketName, string key, string filePath);
    }
}
