using System;
using Shared.Dtos;

namespace Api.Services;

public interface IActivityLogsManagerService
{
    Task<List<ActivityLogDto>> GetLogsAsync(string nodeName);

}
