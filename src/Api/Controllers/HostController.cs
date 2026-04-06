using Api.Exceptions;
using Api.Models;
using Api.Services;
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
            var result = await _proxmoxService.TestConnectionAsync(request);
            if (result)
                return Ok(new { Message = "Connected to Proxmox" });
            return BadRequest(new { Message = "Failed to connect" });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetHostById(Guid id)
        {
            return Ok();
        }

        [HttpPost("register-host")]
        public async Task<IActionResult> RegisterHost(CreateHostRequestDto dto)
        {
            try
            {
                var result = await _hostManagerService.RegisterHostAsync(dto);
                return CreatedAtAction(nameof(GetHostById), new { id = result.Id }, result);
            }
            catch (ProxmoxConnectionException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occured: " + ex.Message);
            }
        }

        [HttpGet("proxmox-hosts")]
        public async Task<IActionResult> GetHosts()
        {
            return Ok(await _hostManagerService.GetHostsAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHost(Guid id, UpdateHostDto dto)
        {
            try
            {
                var result = await _hostManagerService.UpdateHostAsync(id, dto)!;
                if (result is null)
                    return NotFound(new { Message = "Host not found" });
                return Ok(result);
            }
            catch (ProxmoxConnectionException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occured" + ex.Message);
            }
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> SetActiveHost(Guid id)
        {
            var result = await _hostManagerService.SetActiveHostAsync(id);
            if (!result)
                return NotFound(new { Message = "Host not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHost(Guid id)
        {
            try
            {
                var result = await _hostManagerService.DeleteHostAsync(id);
                if (!result)
                    return NotFound(new { Message = "Host not found" });
                return NoContent();
            }
            catch (InvalidHostOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (ProxmoxConnectionException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
