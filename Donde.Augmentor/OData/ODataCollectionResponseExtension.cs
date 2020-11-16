using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.OData
{
    public static class TestExtension
    {
        public static ODataCollectionResponse<object> ToODataCollectionResponse(this IEnumerable<object> result, HttpRequest request)
        {
            return new ODataCollectionResponse<object>(result, request != null ? (long?)request.ODataFeature()?.TotalCount : new long?());
        }    
    }
    public class ODataCollectionResponse<T>
    {
        public ODataCollectionResponse(IEnumerable<T> value, long? totalCount)
        {
            this.Value = value;
            this.TotalCount = totalCount ?? 0L;
        }

        [JsonProperty("@odata.count")]
        public long TotalCount { get; set; }

        public IEnumerable<T> Value { get; set; }
    }
}

