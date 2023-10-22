using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TibberBot.Cleaners;
using TibberBot.Dto;
using TibberBot.Repository;

namespace TibberBot.Controllers
{
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;
        private readonly ICleaner _cleaner;
        private readonly IExecutionsRepository _executionsRepository;

        public BotController(ILogger<BotController> logger, ICleaner cleaner, IExecutionsRepository executionsRepository)
        {
            _logger = logger;
            _cleaner = cleaner;
            _executionsRepository = executionsRepository;
        }

        [Route("/tibber-developer-test/enter-path")]
        [HttpPost]
        public async Task<ActionResult> CleanPath([FromBody]CleanPathRequest request)
        {
            // - validate input e.g., not too many steps or out of bounds
            if (!request.IsValid())
                return StatusCode(StatusCodes.Status400BadRequest, "Not a valid request");

            // - calculate num unique positions
            var record = _cleaner.Clean(request.Start, request.Commands);
            if (record == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Robot failed to clean");

            // - store results in executionrepository
            var storedRecord = await _executionsRepository.RecordExecution(record);
            if (storedRecord == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Result could not be stored");

            return new JsonResult(storedRecord, new JsonSerializerOptions() { WriteIndented = true});
        }

        [Route("/tibber-developer-test/get-records")]
        [HttpGet]
        public async Task<ActionResult> GetCleaningRecords(int limit = 100, string sort = "desc")
        {
            var records = await _executionsRepository.GetExecutions(limit, sort);
            return new JsonResult(records, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
