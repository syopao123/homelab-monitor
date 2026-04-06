using System;

namespace Shared.Dtos;

public class ProxmoxHostDto
{
    public Guid Id { get; set; }
    public required string HostName { get; set; }
    public required string ServerUrl { get; set; }
    public required string ApiToken { get; set; }
    public bool IsActive { get; set; }
}
