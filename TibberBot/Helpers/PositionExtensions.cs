using TibberBot.Dto;

namespace TibberBot.Helpers
{
    public static class PositionExtensions
    {
        public static IEnumerable<Position> EnumerateNewPositions(this Position current, string direction, int steps)
        {
            var posList = new List<Position>();
            for (int i = 0; i < steps; i++)
            {
                var newPos = current.Calculate(direction, 1);
                posList.Add(newPos);
                current = newPos;
            }
            return posList;
        }

        public static Position Calculate(this Position current, string direction, int steps)
        {
            return direction.ToLower() switch
            {
                "north" => new Position(current.X, current.Y + steps),
                "south" => new Position(current.X, current.Y - steps),
                "east" => new Position(current.X + steps, current.Y),
                "west" => new Position(current.X - steps, current.Y),
                _ => current,
            };
        }
    }
}
