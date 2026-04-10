using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Dtos.Nodes;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeManagerService _nodeManager;

        public NodeController(INodeManagerService nodeManager)
        {
            _nodeManager = nodeManager;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<NodeDto>>> GetNodesAsync()
        {
            return Ok(await _nodeManager.GetNodesAsync());
        }

        [HttpPatch("{nodeName}/activate")]
        public async Task<IActionResult> UpdateSelectedNodeAsync(string nodeName)
        {
            var result = await _nodeManager.UpdateSelectedNodeAsync(nodeName);
            if (result is false)
                return NotFound(new { Message = "Node not found" });
            return NoContent();
        }

        // Retrieve info for Dashboard
        [HttpGet("{nodeName}/dashboard")]
        public async Task<ActionResult<NodeDashboardDto>> GetNodeDashboardAsync(string nodeName)
        {
            try
            {
                return Ok(await _nodeManager.GetNodeDashboardAsync(nodeName));
            } catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
