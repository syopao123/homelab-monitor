using System;
using System.Text.Json;
using Api.Data;
using Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Dtos.Storage;

namespace Api.Services;

public class StorageManagerService : IStorageManagerService
{
    private readonly IProxmoxService _proxmoxService;
    private readonly ApplicationDbContext _context;

    public StorageManagerService(IProxmoxService proxmoxService, ApplicationDbContext context)
    {
        _proxmoxService = proxmoxService;
        _context = context;
    }

    public async Task<List<StorageDto>> GetStorageListAsync(string nodeName)
    {
        // Get active host
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync(h => h.IsActive);

        if (activeHost is null)
            throw new InvalidHostOperationException("No active host selected");
        
        if (activeHost.SelectedNodeName != nodeName)
            throw new InvalidNodeOperationException("Current selected node does not match given node name");
        
        // Call proxmox api
        var data = await _proxmoxService.GetStorageListAsync(nodeName, new ProxmoxHostDto
        {
            HostName = activeHost.HostName,
            ServerUrl = activeHost.ServerUrl,
            ApiToken = activeHost.ApiToken
        });

        // Start mapping data
        var storageList = new List<StorageDto>();
        foreach(var item in data.EnumerateArray())
        {
            storageList.Add(new StorageDto
            {
                Storage = item.GetProperty("storage").GetString(),
                Type = item.GetProperty("type").GetString(),
                ContentTypes = item.GetProperty("content").GetString()?.Split(',') ?? Array.Empty<string>(),
                Total = item.GetProperty("total").GetInt64(),
                Used = item.GetProperty("used").GetInt64(),
                Available = item.GetProperty("avail").GetInt64(),
                IsActive = item.GetProperty("active").GetInt32() == 1
            });
        }
        return storageList;
    }
}
