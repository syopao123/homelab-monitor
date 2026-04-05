using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class ProxmoxHost
{
    public int Id { get; set; }

    [Required]
    public required string HostName { get; set; }

    [Required]
    public required string ServerUrl { get; set; }

    [Required]
    public required string ApiToken { get; set; }
}
