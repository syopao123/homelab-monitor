using System;
using Shared.Dtos.Storage;

namespace Web.Services;

public class StorageService : IStorageService
{
    private readonly IHomeLabApiService _apiService;

    public StorageService(IHomeLabApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<List<StorageDto>> GetStoragesAsync(string nodeName)
    {
        return await _apiService.GetStoragesAsync(nodeName);
    }
}
