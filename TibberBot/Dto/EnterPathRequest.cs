using System.Text.Json.Serialization;

namespace TibberBot.Dto
{
    public record EnterPathRequest
    {
        [JsonPropertyName("start")]
        public Position Start { get; set; }
        
        [JsonPropertyName("commands")]
        public IEnumerable<Command> Commands { get; set; } = Array.Empty<Command>(); 

        public EnterPathRequest(Position start, IEnumerable<Command> commands)
        {
            Start = start;
            Commands = commands;
        }
    };

    public record Position(int X, int Y);

    public record Command(string Direction, int Steps);

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}
