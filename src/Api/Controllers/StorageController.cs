using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Storage;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageManagerService _storageManager;
        private readonly NodeContext _nodeContext;

        public StorageController(IStorageManagerService storageManager, NodeContext nodeContext)
        {
            _storageManager = storageManager;
            _nodeContext = nodeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<StorageDto>>> GetStorageListAsync()
        {
            return Ok(await _storageManager.GetStorageListAsync(_nodeContext.ActiveNodeName));
        }
    }
}
