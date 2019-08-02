using System.IO;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IStorageService
    {
        bool UploadFile(string awsBucketName, string key, Stream stream);
    }
}
