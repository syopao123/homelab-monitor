using System.Text.Json;
using Shared.Dtos;

namespace Api.Services;

public interface IProxmoxService
{
    // We use Task because network calls should always be Asynchronous
    Task<bool> TestConnectionAsync(CreateHostRequestDto request);

    Task<JsonElement> GetNodesAsync(string url, string token);

    Task<JsonElement> GetNodeStatusAsync(string nodeName, ProxmoxHostDto host);
    Task<JsonElement> GetNodeVmsAsync(string nodeName, ProxmoxHostDto host);
    Task<JsonElement> GetNodeLxcAsync(string nodeName, ProxmoxHostDto host);

    Task<JsonElement> GetStorageListAsync(string nodeName, ProxmoxHostDto host);
}
