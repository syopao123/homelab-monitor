using System;
using Shared.Dtos.Storage;

namespace Web.Services;

public interface IStorageService
{
    Task<List<StorageDto>> GetStoragesAsync(string nodeName);
}
