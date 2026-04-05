using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : ControllerBase
    {
        private readonly IProxmoxService _service;

        public HostController(IProxmoxService service)
        {
            _service = service;
        }

        [HttpPost("test-connection")]
        public async Task<IActionResult> TestConnection(CreateHostRequestDto request)
        {
            var result = await _service.TestConnectionAsync(request);

            if (result)
            {
                return Ok(new { Message = "Connected to Proxmox" });
            }

            return BadRequest(new { Message = "Failed to connect" });
        }
    }
}
