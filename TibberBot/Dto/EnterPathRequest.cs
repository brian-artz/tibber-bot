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

        public bool IsValid()
        {
            var commandCount = Commands.ToArray().Length;
            return
                (commandCount > 0 && commandCount <= 10_000)
                && (Start.X >= -100_000 && Start.Y >= -100_000)
                && (Start.X <= 100_000 && Start.Y <= 100_000)
                && Commands.All(c => c.Steps > 0 && c.Steps < 100_000);
        }
    };

    public struct Position : IEquatable<Position>
    {
        [JsonPropertyName("x")]
        public int X { get; private set; }
        [JsonPropertyName("y")]
        public int Y { get; private set; }

        [JsonConstructor]
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj) => obj is Position other && Equals(other);

        public readonly bool Equals(Position p) => X == p.X && Y == p.Y;

        public override readonly int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

        public static bool operator ==(Position lhs, Position rhs) => lhs.Equals(rhs);

        public static bool operator !=(Position lhs, Position rhs) => !(lhs == rhs);
    }

    public record Command(string Direction, int Steps);

    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}
