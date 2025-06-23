using System.Text.Json.Serialization;

namespace macmod.services.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MapEnvironment
{
    Unknown,
    SciFi,
    Ancient,
    Industrial,
    Glacier,
    Fantasy,
    Realism,
}