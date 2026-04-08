using System.Text.Json;
using Shared.Dtos;

namespace Api.Services;

public interface IProxmoxService
{
    // We use Task because network calls should always be Asynchronous
    Task<bool> TestConnectionAsync(CreateHostRequestDto request);

    Task<JsonElement> GetNodesAsync(string url, string token);
    Task<NodeDto> GetNodeInfoAsync(string url, string token, string nodeName);

}
