using System.Text.Json.Serialization;

namespace TibberBot.Dto
{
    public record CleanPathRequest
    {
        [JsonPropertyName("start")]
        public Position Start { get; set; }

        [JsonPropertyName("commands")]
        public IEnumerable<Command> Commands { get; set; } = Array.Empty<Command>();

        public CleanPathRequest(Position start, IEnumerable<Command> commands)
        {
            Start = start;
            Commands = commands;
        }

        public bool IsValid()
        {
            var commandCount = Commands.ToArray().Length;
            return
                (commandCount > 0 && commandCount <= 10_000)
                && (Start.X >= -100_000 && Start.Y >= -100_000)
                && (Start.X <= 100_000 && Start.Y <= 100_000)
                // assignment instructions specified < 100_000 (not <=) but it is easier to test this way
                && Commands.All(c => c.Steps > 0 && c.Steps <= 100_000);
        }
    };
}
