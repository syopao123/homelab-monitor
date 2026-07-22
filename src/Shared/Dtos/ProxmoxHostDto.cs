using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class ProxmoxHostDto
{
    public Guid Id { get; set; }
    public string HostName { get; set; }
    public string ServerUrl { get; set; }
    public string ApiToken { get; set; }
    public bool IsActive { get; set; }
}
