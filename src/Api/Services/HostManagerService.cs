using System;
using Api.Data;
using Api.Exceptions;
using Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Api.Services;

public class HostManagerService : IHostManagerService
{
    private readonly IProxmoxService _proxmoxService;
    private readonly ApplicationDbContext _context;

    public HostManagerService(IProxmoxService proxmoxService, ApplicationDbContext context)
    {
        _proxmoxService = proxmoxService;
        _context = context;
    }

    public async Task<ProxmoxHost> RegisterHostAsync(CreateHostRequestDto dto)
    {
        var isReachable = await _proxmoxService.TestConnectionAsync(dto);
        if (!isReachable)
        {
            throw new ProxmoxConnectionException(
                $"Failed to reach Proxmox node at {dto.ServerUrl}."
            );
        }
        var newHost = new ProxmoxHost
        {
            HostName = dto.HostName,
            ServerUrl = $"https://{dto.ServerUrl}/",
            ApiToken = dto.ApiToken,
            CreatedAt = DateTime.UtcNow,
            IsActive = false,
        };
        _context.ProxmoxHosts.Add(newHost);
        await _context.SaveChangesAsync();
        return newHost;
    }

    public async Task<IEnumerable<ProxmoxHost>> GetHostsAsync()
    {
        return await _context.ProxmoxHosts.ToListAsync();
    }

    public async Task<ProxmoxHost>? UpdateHostAsync(Guid id, UpdateHostDto dto)
    {
        // Find the host
        var host = await _context.ProxmoxHosts.FirstOrDefaultAsync(h => h.Id == id);
        if (host is null)
        {
            // Return null if host is not found
            return null!;
        }
        var finalHostName = string.IsNullOrWhiteSpace(dto.HostName) ? host.HostName : dto.HostName;
        var finalServerUrl = string.IsNullOrWhiteSpace(dto.ServerUrl)
            ? host.ServerUrl
            : dto.ServerUrl;
        var finalApiToken = string.IsNullOrWhiteSpace(dto.ApiToken) ? host.ApiToken : dto.ApiToken;
        // Verify connection
        var createHostRequest = new CreateHostRequestDto
        {
            HostName = finalHostName,
            ServerUrl = finalServerUrl,
            ApiToken = finalApiToken,
        };
        var isReachable = await _proxmoxService.TestConnectionAsync(createHostRequest);
        if (!isReachable)
            throw new ProxmoxConnectionException(
                $"Failed to reach Proxmox node at {createHostRequest.ServerUrl} ({createHostRequest.HostName}, {createHostRequest.ApiToken})."
            );
        // Proceed to updating host
        host.HostName = finalHostName;
        host.ServerUrl = finalServerUrl;
        host.ApiToken = finalApiToken;
        await _context.SaveChangesAsync();

        return host;
    }

    public async Task<bool> SetActiveHostAsync(Guid id)
    {
        // Find the host
        var newActiveHost = await _context.ProxmoxHosts.FindAsync(id);
        if (newActiveHost is null)
            return false;
        if (newActiveHost.IsActive)
            return true;
        // Set all hosts to inactive
        var hosts = await _context.ProxmoxHosts.Where(h => h.IsActive == true).ToListAsync();
        foreach (var host in hosts)
        {
            host.IsActive = false;
        }

        newActiveHost.IsActive = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteHostAsync(Guid id)
    {
        var host = await _context.ProxmoxHosts.FindAsync(id);
        if (host is null)
            return false;
        if (host.IsActive)
            throw new InvalidHostOperationException("Cannot delete active host.");
        _context.ProxmoxHosts.Remove(host);
        await _context.SaveChangesAsync();
        return true;
    }
}
