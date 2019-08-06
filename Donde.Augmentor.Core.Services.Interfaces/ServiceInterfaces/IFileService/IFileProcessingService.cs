using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService
{
    public interface IFileProcessingService
    {
        Task<bool> UploadImageAsync(HttpRequest request);
    }
}
