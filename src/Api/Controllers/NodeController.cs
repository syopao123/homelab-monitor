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
        private readonly NodeContext _nodeContext;

        public NodeController(INodeManagerService nodeManager, NodeContext nodeContext)
        {
            _nodeManager = nodeManager;
            _nodeContext = nodeContext;
        }

        [HttpGet("active-node")]
        public async Task<ActionResult<string>> GetActiveNodeName()
        {
            var result = await _nodeManager.GetActiveNodeNameAsync();
            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<NodeDto>>> GetNodes()
        {
            return Ok(await _nodeManager.GetNodesAsync());
        }

        [HttpPatch("{nodeName}/activate")]
        public async Task<IActionResult> ActivateNode(string nodeName)
        {
            await _nodeManager.ActivateNodeAsync(nodeName);
            return NoContent();
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<NodeDashboardDto>> GetNodeDashboardAsync()
        {
            return Ok(await _nodeManager.GetNodeDashboardAsync(_nodeContext.ActiveNodeName));
        }
    }
}
