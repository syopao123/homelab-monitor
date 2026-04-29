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

        public ResourcesController(IResourcesManagerService resourcesManager)
        {
            _resourcesManager = resourcesManager;
        }

        [HttpGet("{nodeName}")]
        public async Task<ActionResult<List<WorkloadDto>>> GetResourcesAsync(string nodeName)
        {
            var result = await _resourcesManager.GetResourcesAsync(nodeName);
            return Ok(result);
        }
    }
}
