using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardManagerService _dashboardManager;

        public DashboardController(IDashboardManagerService dashboardManager)
        {
            _dashboardManager = dashboardManager;
        }

        // TODO: Update frontend UI to select which node to select from the active host (Settings)
        [HttpGet("{nodeName}/info")]
        public async Task<ActionResult<NodeDto>> GetNodeInfoAsync(string nodeName)
        {
            try
            {
                var nodeInfo = await _dashboardManager.GetNodeInformationAsync(nodeName);
                return nodeInfo;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
