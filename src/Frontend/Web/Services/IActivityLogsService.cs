using System;
using Shared.Dtos;

namespace Web.Services;

public interface IActivityLogsService
{
    Task<List<ActivityLogDto>> GetActivityLogsAsync(string nodeName);

}
