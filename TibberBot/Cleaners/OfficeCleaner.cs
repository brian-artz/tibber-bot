using System.Diagnostics;
using TibberBot.Dto;
using TibberBot.Helpers;

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
            if (!commands.Any())
            {
                _logger.LogInformation("No commands given. I will go back to sleep...");
                return null;
            }

            var uniqueSpacesCleaned = new HashSet<Position>() { start };
            var currentPos = start;
            _logger.LogInformation("Starting new cleaning job at position: {pos}", start);
            var stopWatch = Stopwatch.StartNew();
            int commandsRun = 0;

            try
            {
                foreach (var command in commands)
                {
                    _logger.LogInformation("Calculating next move...");
                    var nextPos = CalculateNext(currentPos, command);

                    if (!IsInBounds(nextPos))
                        throw new InvalidOperationException("Robot went out of bounds");

                    // BUG: it's possible that some vertices/positions have not been visited but the ending pos has already been traversed
                    // i.e. perfect squares will be missing a side in hashset...
                    // can fix by enumerating pos and adding them all to the hashset, which will still not allow dups
                    // but then an add is always performed for each position i.e., not as efficient
                    //if (!uniqueSpacesCleaned.Contains(newPos))
                    //{
                    //    _logger.LogInformation("Cleaning new area...");
                    //    var newArea = EnumerateNewPositions(currentPos, command);
                    //    uniqueSpacesCleaned.UnionWith(newArea);
                    //}
                    //else
                    //{
                    //    _logger.LogInformation("Already cleaned this area");
                    //}

                    _logger.LogInformation("Cleaning office area...");
                    var newArea = EnumeratePositions(currentPos, command);
                    uniqueSpacesCleaned.UnionWith(newArea);

                    currentPos = nextPos;
                    commandsRun++;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Critical failure while cleaning office: {ex}", ex);
                return null;
            }

            stopWatch.Stop();
            _logger.LogInformation("Finished cleaning at final position: {pos}", currentPos);
            return new ExecutionRecord()
            {
                StartingPosition = start,
                Commands = commandsRun,
                Result = uniqueSpacesCleaned.Count,
                Duration = stopWatch.Elapsed.TotalSeconds,
                EndingPosition = currentPos
            };
        }

        private static bool IsInBounds(Position p)
        {
            return (p.X >= _lowerLeftBoundary.X && p.Y >= _lowerLeftBoundary.Y)
                && (p.X <= _upperRightBoundary.X && p.Y <= _upperRightBoundary.Y);
        }

        private static Position CalculateNext(Position current, Command command)
        {
            return current.CalculateNext(command.Direction, command.Steps);
        }

        private static IEnumerable<Position> EnumeratePositions(Position current, Command command)
        {
            return current.EnumeratePositions(command.Direction, command.Steps);
        }
    }
}
