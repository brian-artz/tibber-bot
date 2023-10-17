using Microsoft.AspNetCore.Mvc;
using TibberBot.Dto;
using TibberBot.Repository;

namespace TibberBot.Controllers
{
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly ILogger<RobotController> _logger;
        private readonly IExecutionsRepository _executionsRepository;

        public RobotController(ILogger<RobotController> logger, IExecutionsRepository executionsRepository)
        {
            _logger = logger;
            _executionsRepository = executionsRepository;
        }

        [Route("/tibber-developer-test/enter-path")]
        [HttpPost]
        public async Task<ActionResult> CleanPath([FromBody]EnterPathRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
