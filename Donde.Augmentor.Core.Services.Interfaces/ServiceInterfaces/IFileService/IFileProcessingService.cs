using CSharpFunctionalExtensions;
using Donde.Augmentor.Core.Domain.Dto;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService
{
    public interface IFileProcessingService
    {
        Task<Result<MediaAttachmentDto>> UploadImageAsync(HttpRequest request);
    }
}
