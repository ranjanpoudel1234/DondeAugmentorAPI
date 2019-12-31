using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Donde.Augmentor.Core.Domain.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AugmentObjectMediaTypes
    {
        AvatarWithAudio = 0,
        Video = 1
    }
}
