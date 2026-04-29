using Api.Exceptions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : ControllerBase
    {
        private readonly IProxmoxService _proxmoxService;
        private readonly IHostManagerService _hostManagerService;

        public HostController(
            IProxmoxService proxmoxService,
            IHostManagerService hostManagerService
        )
        {
            _proxmoxService = proxmoxService;
            _hostManagerService = hostManagerService;
        }

        // Tests the connection of a given Proxmox Host
        [HttpPost("test-connection")]
        public async Task<IActionResult> TestConnection(CreateHostRequestDto request)
        {
            await _proxmoxService.TestConnectionAsync(request);
            return Ok(new { Message = "Connected to Proxmox" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProxmoxHostDto>> GetHostById(Guid id)
        {
            return Ok(await _hostManagerService.GetHostByIdAsync(id));
        }

        [HttpPost("register-host")]
        public async Task<ActionResult<ProxmoxHostDto>> RegisterHost(CreateHostRequestDto dto)
        {
            var result = await _hostManagerService.RegisterHostAsync(dto);
            return CreatedAtAction(nameof(GetHostById), new { id = result.Id }, result);
        }

        [HttpGet("proxmox-hosts")]
        public async Task<IActionResult> GetHosts()
        {
            return Ok(await _hostManagerService.GetHostsAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHost(Guid id, UpdateHostDto dto)
        {
            return Ok(await _hostManagerService.UpdateHostAsync(id, dto)!);
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> SetActiveHost(Guid id)
        {
            await _hostManagerService.SetActiveHostAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHost(Guid id)
        {
            await _hostManagerService.DeleteHostAsync(id);
            return NoContent();
        }
    }
}
