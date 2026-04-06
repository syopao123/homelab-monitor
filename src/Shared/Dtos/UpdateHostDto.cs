using System;

namespace Shared.Dtos;

public class UpdateHostDto
{
    public string? HostName { get; set; }
    public string? ServerUrl { get; set; }
    public string? ApiToken { get; set; }
}
