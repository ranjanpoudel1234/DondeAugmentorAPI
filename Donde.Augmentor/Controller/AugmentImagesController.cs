using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.Attributes;
using Donde.Augmentor.Web.Helpers;
using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentImages")]
    public class AugmentImagesController : ODataController
    {
        private readonly IAugmentImageService _augmentImageservice;
        private readonly IMapper _mapper;
        private readonly ILogger<AugmentImagesController> _logger;
        private readonly IStorageService _storageService;

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public AugmentImagesController(IAugmentImageService augmentImageservice, IMapper mapper, IStorageService storageService, ILogger<AugmentImagesController> logger)
        {
            _augmentImageservice = augmentImageservice;
            _mapper = mapper;
            _logger = logger;
            _storageService = storageService;
        }

        [ODataRoute]
        [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<AugmentImageViewModel> odataOptions)
        {
            var result = new List<AugmentImageViewModel>();

            var augmentImagesQueryable = _augmentImageservice.GetAugmentImages();

            var projectedAugmentImages = augmentImagesQueryable.ProjectTo<AugmentImageViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedAugmentImages);

            var augmentImageViewModels = appliedResults as IQueryable<AugmentImageViewModel>;

            if (augmentImageViewModels != null)
            {
                result = await augmentImageViewModels.ToListAsync();
            }

            return Ok(result);
        }


        [ODataRoute]
        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(100000000)] // 100 mb
        public async Task<IActionResult> Upload()
        {
            Stream stream  = await new FileStreamingHelper().StreamFile(Request);
            ReadFully2(stream, 0);

            return Ok();
        }

        private void ChunkStream(Stream stream)
        {
            byte[] chunk = new byte[5000];
            while (true)
            {
                int index = 0;
                // There are various different ways of structuring this bit of code.
                // Fundamentally we're trying to keep reading in to our chunk until
                // either we reach the end of the stream, or we've read everything we need.
                while (index < chunk.Length)
                {
                    int bytesRead = stream.Read(chunk, index, chunk.Length - index);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    index += bytesRead;
                  
                }
                if (index != 0) // Our previous chunk may have been the last one
                {
                    //call back here
                    var chunkStream = new MemoryStream();
                    chunkStream.Write(chunk, 0, index); //write chunk to [wherever]
                    _storageService.UploadFile("bucketofpankaj", "testKey", chunkStream);
                    //chunkStream.WriteByte(bytesRead); //write chunk to [wherever]

                    // SendChunk(chunk, index); // index is the number of bytes in the chunk
                }
                if (index != chunk.Length) // We didn't read a full chunk: we're done
                {
                    return;
                }
            }
        }

        public void ReadFully(Stream stream)
        {
            byte[] buffer = new byte[5000000]; //set the size of your buffer (chunk)
            
                while (true) //loop to the end of the file
                {
                    var chunkStream = new MemoryStream();
                        int read = stream.Read(buffer, 0, buffer.Length); //read each chunk
                    if (read <= 0) //check for end of file
                        break;

                chunkStream.Write(buffer, 0, read); //write chunk to [wherever]
                _storageService.UploadFile("bucketofpankaj", "testKey", chunkStream);
            }
            
        }
        
        private void ReadAnother(Stream stream)
        {
            var b = new byte[500000]; // 32k
            int count = 0;
            while ((count = stream.Read(b, 0, b.Length)) > 0)
            {
                var chunkStream = new MemoryStream();
                chunkStream.Write(b, 0, count);
                _storageService.UploadFile("bucketofpankaj", "testKey", chunkStream);
            }
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        public byte[] ReadFully2(Stream stream, int initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;

                  

                    read++;

               
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);

            var chunkStream = new MemoryStream();
            chunkStream.Write(ret, 0, ret.Length);
            _storageService.UploadFile("bucketofpankaj", "testKey", chunkStream);

            //return ret;
            return null;
        }

        private void CreateFile(Stream stream)
        {
            string path = @"C:\temp\Stay.png";

            try
            {

                // Delete the file if it exists.
                if (System.IO.File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    System.IO.File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = System.IO.File.Create(path))
                {
                    stream.CopyTo(fs);
                }

                // Open the stream and read it back.
                using (StreamReader sr = System.IO.File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
}
