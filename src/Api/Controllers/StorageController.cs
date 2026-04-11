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

        public StorageController(IStorageManagerService storageManager)
        {
            _storageManager = storageManager;
        }

        [HttpGet("{nodeName}")]
        public async Task<ActionResult<List<StorageDto>>> GetStorageListAsync(string nodeName)
        {
            var result = await _storageManager.GetStorageListAsync(nodeName);
            return Ok(result);
        }
    }
}
