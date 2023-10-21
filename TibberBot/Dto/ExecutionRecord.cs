using System.Text.Json.Serialization;

namespace TibberBot.Dto
{
    /// <summary>
    /// A summary of events during a cleaning job, meant for storing in a database
    /// </summary>
    public record ExecutionRecord
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime TimeStamp { get; set; }
        [JsonPropertyName("commands")]
        public int Commands { get; set; }
        [JsonPropertyName("result")]
        public int Result { get; set; }
        [JsonPropertyName("duration")]
        public double Duration { get; set; }

        public override string ToString()
        {
            return $"{Commands},{Result},{Duration}";
        }
    }
}
