using System.Diagnostics;
using TibberBot.Dto;

namespace TibberBot.Cleaners
{
    public class OfficeCleaner : ICleaner
    {
        private readonly ILogger<OfficeCleaner> _logger;
        private static readonly Position _lowerLeftBoundary = new(-100_000, -100_000);
        private static readonly Position _upperRightBoundary = new(100_000, 100_000);

        public OfficeCleaner(ILogger<OfficeCleaner> logger)
        {
            _logger = logger;
        }

        public ExecutionRecord? Clean(Position start, IEnumerable<Command> commands)
        {
            var uniqueSpacesCleaned = new HashSet<Position>() { start };
            var currentPos = start;
            var stopWatch = Stopwatch.StartNew();
            int commandsRun = 0;

            try
            {
                foreach (var command in commands)
                {
                    _logger.LogInformation("Calculating next move...");
                    var newPos = Calculate(currentPos, command);
                    if (!IsInBounds(newPos))
                        throw new InvalidOperationException("Robot went out of bounds");
                    if (!uniqueSpacesCleaned.Contains(newPos))
                    {
                        _logger.LogInformation("Cleaning new area...");
                        var newArea = EnumerateNewPositions(currentPos, command);
                        uniqueSpacesCleaned.UnionWith(newArea);
                    }
                    currentPos = newPos;
                    commandsRun++;
                }
                // foreach command
                // - calculate end position
                // - check if hashset has end position
                // - - if yes, set currentPosition & go to next command
                // - - if no, add positions to hashset
                // - set currentPosition = calculated end position
            }
            catch (Exception ex)
            {
                _logger.LogError("Critical failure while cleaning office: {ex}", ex);
                return null;
            }

            stopWatch.Stop();
            return new ExecutionRecord()
            {
                Commands = commandsRun,
                Result = uniqueSpacesCleaned.Count,
                Duration = stopWatch.Elapsed.TotalSeconds
            };
        }

        private static bool IsInBounds(Position p)
        {
            return (p.X >= _lowerLeftBoundary.X && p.Y >= _lowerLeftBoundary.Y)
                && (p.X <= _upperRightBoundary.X && p.Y <= _upperRightBoundary.Y);
        }

        private static Position Calculate(Position current, Command command)
        {
            return command.Direction.ToLower() switch
            {
                "north" => new Position(current.X, current.Y + command.Steps),
                "south" => new Position(current.X, current.Y - command.Steps),
                "east" => new Position(current.X + command.Steps, current.Y),
                "west" => new Position(current.X - command.Steps, current.Y),
                _ => current,
            };
        }

        private static IEnumerable<Position> EnumerateNewPositions(Position current, Command command)
        {
            var posList = new List<Position>();
            for (int i = 0; i < command.Steps; i++)
            {
                var newPos = command.Direction.ToLower() switch
                {
                    "north" => new Position(current.X, current.Y + 1),
                    "south" => new Position(current.X, current.Y - 1),
                    "east" => new Position(current.X + 1, current.Y),
                    "west" => new Position(current.X - 1, current.Y),
                    _ => current,
                };
                posList.Add(newPos);
                current = newPos;
            }
            return posList;
        }
    }
}
