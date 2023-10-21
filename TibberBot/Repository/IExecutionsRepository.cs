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
        Task<IEnumerable<ExecutionRecord>> GetAllExecutions();
    }
}
