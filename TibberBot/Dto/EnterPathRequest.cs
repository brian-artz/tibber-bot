using System.Text.Json.Serialization;

namespace TibberBot.Dto
{
    public record EnterPathRequest
    {
        [JsonPropertyName("start")]
        public Start Start { get; set; }
        
        [JsonPropertyName("commands")]
        public IEnumerable<Command> Commands { get; set; } = Array.Empty<Command>(); 

        public EnterPathRequest(Start start, IEnumerable<Command> commands)
        {
            Start = start;
            Commands = commands;
        }
    };

    public record Start(int X, int Y);

    public record Command(string Direction, int Steps);

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}
