using System.Text.Json.Serialization;

namespace LesApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        CLIENT,
        ADMIN,
        AGENT
    }

}
