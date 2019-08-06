using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService
{
    public interface IFileStreamContentReaderService
    {
        Task<bool> StreamFileAsync(HttpRequest request);
        Stream Stream { get; set; }
        string FileName { get; set; }
        string MimeType { get; set; }
    }
}
