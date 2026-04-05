using System;

namespace Shared.Dtos;

public class CreateHostRequestDto
{
    public required string HostName { get; set; }
    public required string ServerUrl { get; set; }
    public required string ApiToken { get; set; }
}
