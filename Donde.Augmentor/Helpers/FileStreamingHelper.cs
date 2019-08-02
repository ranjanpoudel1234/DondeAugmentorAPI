using Donde.Augmentor.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public static class FileStreamingHelper
{ 
    private static readonly FormOptions _defaultFormOptions = new FormOptions();

    public static async Task<Stream> StreamFile(this HttpRequest request, Stream targetStream)
    {
        if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
        {
            throw new Exception($"Expected a multipart request, but got {request.ContentType}");
        }

        var boundary = MultipartRequestHelper.GetBoundary(
            MediaTypeHeaderValue.Parse(request.ContentType),
            _defaultFormOptions.MultipartBoundaryLengthLimit);

        var reader = new MultipartReader(boundary, request.Body);

        var section = await reader.ReadNextSectionAsync();
        while (section != null)
        {
            ContentDispositionHeaderValue contentDisposition;
            var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

            if (hasContentDispositionHeader)
            {
                if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                {
                    await section.Body.CopyToAsync(targetStream);
                }       
            }

            // Drains any remaining section body that has not been consumed and
            // reads the headers for the next section.
            section = await reader.ReadNextSectionAsync();
        }

        return targetStream;
    }

    private static Encoding GetEncoding(MultipartSection section)
    {
        MediaTypeHeaderValue mediaType;
        var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
        // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
        // most cases.
        if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
        {
            return Encoding.UTF8;
        }
        return mediaType.Encoding;
    }
}