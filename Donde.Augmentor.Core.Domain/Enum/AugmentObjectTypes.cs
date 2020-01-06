using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Donde.Augmentor.Core.Domain.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AugmentObjectTypes
    {
        Static = 0,
        Geographical = 1
    }
}
