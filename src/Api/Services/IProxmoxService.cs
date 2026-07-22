using System.Text.Json;
using Shared.Dtos;

namespace Api.Services;

public interface IProxmoxService
{
    Task<bool> TestConnectionAsync(string serverUrl, string apiToken);

    Task<JsonElement> GetNodesAsync(string url, string token);

    Task<JsonElement> GetNodeStatusAsync(string nodeName, ProxmoxHostDto host);
    Task<JsonElement> GetNodeVmsAsync(string nodeName, ProxmoxHostDto host);
    Task<JsonElement> GetNodeLxcAsync(string nodeName, ProxmoxHostDto host);

    Task<JsonElement> GetStorageListAsync(string nodeName, ProxmoxHostDto host);

    Task<JsonElement> GetLogsAsync(string nodeName, ProxmoxHostDto host);
}
