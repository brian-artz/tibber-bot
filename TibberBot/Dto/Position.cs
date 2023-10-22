namespace TibberBot.Dto
{
    public readonly record struct Position(int X, int Y)
    {
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    // This is what I originally had before discovering the amazing record struct feature

    //public struct Position : IEquatable<Position>
    //{
    //    [JsonPropertyName("x")]
    //    public int X { get; private set; }
    //    [JsonPropertyName("y")]
    //    public int Y { get; private set; }

    //    [JsonConstructor]
    //    public Position(int x, int y)
    //    {
    //        X = x;
    //        Y = y;
    //    }

    //    public override bool Equals(object? obj) => obj is Position other && Equals(other);

    //    public readonly bool Equals(Position p) => X == p.X && Y == p.Y;

    //    public override readonly int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

    //    public static bool operator ==(Position lhs, Position rhs) => lhs.Equals(rhs);

    //    public static bool operator !=(Position lhs, Position rhs) => !(lhs == rhs);
    //}
}
