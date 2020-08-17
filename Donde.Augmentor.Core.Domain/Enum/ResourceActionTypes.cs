using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Donde.Augmentor.Core.Domain.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResourceActionTypes
    {      
        Create = 0,
        Read = 1,
        Update = 2,
        Delete = 3,        
    }
}
