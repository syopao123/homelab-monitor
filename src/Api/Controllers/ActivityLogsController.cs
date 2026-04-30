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
        private readonly NodeContext _nodeContext;

        public ActivityLogsController(IActivityLogsManagerService logsManager, NodeContext nodeContext)
        {
            _logsManager = logsManager;
            _nodeContext = nodeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActivityLogDto>>> GetLogs()
        {
            return Ok(await _logsManager.GetLogsAsync(_nodeContext.ActiveNodeName));
        }
    }
}
