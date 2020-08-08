using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Donde.Augmentor.Core.Domain.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResourceTypes
    {
        Users = 0,
        Organizations = 1, 
        Targets = 2
    }
}
