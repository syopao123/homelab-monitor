using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class CreateHostDto
{
    [Required(ErrorMessage = "Host name is required")]
    public string HostName { get; set; }
    [Required(ErrorMessage = "Server URL is required")]
    public string ServerUrl { get; set; }
    [Required(ErrorMessage = "API token is required")]
    public string ApiToken { get; set; }
}
