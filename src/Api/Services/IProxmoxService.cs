using Shared.Dtos;

namespace Api.Services;

public interface IProxmoxService
{
    // We use Task because network calls should always be Asynchronous
    Task<bool> TestConnectionAsync(CreateHostRequestDto request);
    Task<NodeInfoDto> GetNodeInfoAsync(string url, string token, string nodeName);
}
