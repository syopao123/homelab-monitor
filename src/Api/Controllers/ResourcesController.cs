using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesManagerService _resourcesManager;
        private readonly NodeContext _nodeContext;

        public ResourcesController(IResourcesManagerService resourcesManager, NodeContext nodeContext)
        {
            _resourcesManager = resourcesManager;
            _nodeContext = nodeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkloadDto>>> GetResourcesAsync()
        {
            return Ok(await _resourcesManager.GetResourcesAsync(_nodeContext.ActiveNodeName));
        }
    }
}
