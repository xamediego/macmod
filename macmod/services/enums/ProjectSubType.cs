using System.Text.Json.Serialization;

namespace macmod.services.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProjectSubType
{
    GAMEMAP,
    PROGRAMMINGPROJECT
}