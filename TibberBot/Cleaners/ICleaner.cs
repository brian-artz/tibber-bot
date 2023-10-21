using TibberBot.Dto;

namespace TibberBot.Cleaners
{
    public interface ICleaner
    {
        /// <summary>
        /// Cleans a 2D space
        /// </summary>
        /// <param name="start">The starting position to begin cleaning from</param>
        /// <param name="commands">A collection of commands informing the cleaner where to clean next</param>
        /// <returns>A summary of the cleaning job (or null if an error occurs), including the number of unique spaces cleaned as well as the duration of the cleaning job</returns>
        ExecutionRecord? Clean(Position start, IEnumerable<Command> commands);
    }
}
