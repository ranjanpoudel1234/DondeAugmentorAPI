using Donde.Augmentor.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class FileStreamingHelper
{ 
    private static readonly FormOptions _defaultFormOptions = new FormOptions();
    public static Stream Stream { get; set; }

    public async Task<Stream> StreamFile( HttpRequest request)
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
        ContentDispositionHeaderValue contentDisposition;
        var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

        if (hasContentDispositionHeader)
        {
            if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
            {
                Stream = section.Body;
                var test = contentDisposition.FileName.ToString();
                var mimeType = section.ContentType;
            }       
        }

        return Stream;
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