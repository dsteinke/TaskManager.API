using System.Text.Json.Serialization;

namespace TaskManager.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PriorityLevel
    {
        low = 0,
        middle = 1,
        high = 2,
        critical = 3
    }
}
