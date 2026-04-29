using Api.Exceptions;
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

        [HttpGet("active-node")]
        public async Task<ActionResult<string>> GetSelectedNodeNameAsync()
        {
            var result = await _nodeManager.GetSelectedNodeNameAsync();
            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<NodeDto>>> GetNodesAsync()
        {
            return Ok(await _nodeManager.GetNodesAsync());
        }

        [HttpPatch("{nodeName}/activate")]
        public async Task<IActionResult> UpdateSelectedNodeAsync(string nodeName)
        {
            await _nodeManager.UpdateSelectedNodeAsync(nodeName);
            return NoContent();
        }

        // Retrieve info for Dashboard
        [HttpGet("{nodeName}/dashboard")]
        public async Task<ActionResult<NodeDashboardDto>> GetNodeDashboardAsync(string nodeName)
        {
            return Ok(await _nodeManager.GetNodeDashboardAsync(nodeName));
        }

        [HttpGet("test-error")]
        public IActionResult GetTestError()
        {
            throw new InvalidNodeOperationException("This is a test error from the middleware");
        }
    }
}
