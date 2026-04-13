using System;
using Shared.Dtos;

namespace Web.Services;

public class ActivityLogsService : IActivityLogsService
{
    private readonly IHomeLabApiService _apiService;

    public ActivityLogsService(IHomeLabApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<List<ActivityLogDto>> GetActivityLogsAsync(string nodeName)
    {
        return await _apiService.GetActivityLogsAsync(nodeName);        
    }
}
