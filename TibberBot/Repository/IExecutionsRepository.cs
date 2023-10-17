using TibberBot.Dto;

namespace TibberBot.Repository
{
    public interface IExecutionsRepository
    {
        Task<bool> RecordExecutions(IEnumerable<ExecutionRecord> executions);
        Task<IEnumerable<ExecutionRecord>> GetExecutions();
    }
}
