using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Donde.Augmentor.Core.Domain.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrganizationTypes
    {
        University = 0
    }
}
