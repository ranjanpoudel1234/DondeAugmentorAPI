using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Donde.Augmentor.Core.Domain.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaTypes
    {
        Image = 0,
        Video = 1,
        Audio = 2,
        Logo
    }
}
