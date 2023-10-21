using Npgsql;
using TibberBot.Dto;

namespace TibberBot.Repository
{
    public class ExecutionsRepository : IExecutionsRepository
    {
        private readonly ILogger<ExecutionsRepository> _logger;
        private readonly NpgsqlDataSource _dataSource;

        public ExecutionsRepository(ILogger<ExecutionsRepository> logger, NpgsqlDataSource dataSource)
        {
            _logger = logger;
            _dataSource = dataSource;
        }

        public async Task<ExecutionRecord?> RecordExecution(ExecutionRecord execution)
        {
            try
            {
                await using var cmd = _dataSource.CreateCommand(
                        "INSERT INTO executions (commands, result, duration) " +
                        "VALUES (@commands, @result, @duration)" +
                        "RETURNING id, timestamp");
                cmd.Parameters.AddWithValue("commands", execution.Commands);
                cmd.Parameters.AddWithValue("result", execution.Result);
                cmd.Parameters.AddWithValue("duration", execution.Duration);

                _logger.LogDebug("SQL command to be executed: {cmd} with params {params}", cmd.CommandText, execution.ToString());

                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var id = reader.GetInt64(0);
                    var timestamp = reader.GetDateTime(1);
                    execution = execution with { Id = id, TimeStamp = timestamp };
                    _logger.LogInformation("New row inserted into executions table with id: {id}", id);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to insert to db: {error}", ex);
                return null;
            }

            return execution;
        }

        public Task<IEnumerable<ExecutionRecord>> GetAllExecutions()
        {
            //todo
            throw new NotImplementedException();
        }
    }
}
