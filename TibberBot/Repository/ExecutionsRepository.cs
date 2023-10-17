using Npgsql;
using TibberBot.Dto;

namespace TibberBot.Repository
{
    public class ExecutionsRepository : IExecutionsRepository
    {
        private readonly ILogger<ExecutionsRepository> _logger;
        private readonly string _connectionString;

        public ExecutionsRepository(ILogger<ExecutionsRepository> logger, string connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public async Task<bool> RecordExecutions(IEnumerable<ExecutionRecord> executions)
        {
            int rowsInserted = 0;
            await using var conn = new NpgsqlConnection(_connectionString);
            try
            {
                await conn.OpenAsync();
                var batch = new NpgsqlBatch(conn);

                foreach (var execution in executions)
                {
                    var cmd = new NpgsqlBatchCommand(
                        "INSERT INTO executions (timestamp, commands, result, duration) " +
                        "VALUES (@timestamp, @commands, @result, @duration)");
                    cmd.Parameters.AddWithValue("timestamp", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("commands", execution.Commands);
                    cmd.Parameters.AddWithValue("result", execution.Result);
                    cmd.Parameters.AddWithValue("duration", execution.Duration);
                    
                    batch.BatchCommands.Add(cmd);
                    _logger.LogDebug("SQL command added to batch: {cmd} with params {params}", cmd.CommandText, execution.ToString());
                }

                var newRows = await batch.ExecuteNonQueryAsync();
                rowsInserted += newRows;
                _logger.LogInformation("{num} new rows inserted into executions table", newRows);
            }
            catch (Exception ex)
            {

                _logger.LogError("Failed to insert to db: {error}", ex);
            }
            finally
            {
                await conn.CloseAsync();
            }

            return rowsInserted > 0;
        }

        public Task<IEnumerable<ExecutionRecord>> GetExecutions()
        {
            //todo
            throw new NotImplementedException();
        }
    }
}
