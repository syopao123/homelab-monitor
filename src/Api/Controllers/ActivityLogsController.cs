using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogsController : ControllerBase
    {
        private readonly IActivityLogsManagerService _logsManager;

        public ActivityLogsController(IActivityLogsManagerService logsManager)
        {
            _logsManager = logsManager;
        }

        [HttpGet("{nodeName}")]
        public async Task<ActionResult<List<ActivityLogDto>>> GetLogsAsync(string nodeName)
        {
            var result = await _logsManager.GetLogsAsync(nodeName);
            return Ok(result);
        }
    }
}
