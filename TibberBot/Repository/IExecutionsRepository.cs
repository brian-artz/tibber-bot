using TibberBot.Dto;

namespace TibberBot.Repository
{
    public interface IExecutionsRepository
    {
        /// <summary>
        /// Stores an <see cref="ExecutionRecord"/> in a database
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        Task<ExecutionRecord?> RecordExecution(ExecutionRecord execution);

        /// <summary>
        /// Fetches <see cref="ExecutionRecord"/> from the database
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<IEnumerable<ExecutionRecord>> GetExecutions(int limit = 100, string sort = "desc");
    }
}
