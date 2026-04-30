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

    public async Task<List<ActivityLogDto>> GetLogsAsync()
    {
        return await _apiService.GetActivityLogsAsync();        
    }
}
