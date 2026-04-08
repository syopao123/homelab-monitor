using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

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
    }
}
