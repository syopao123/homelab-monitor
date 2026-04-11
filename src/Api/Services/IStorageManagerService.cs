using System;
using Shared.Dtos.Storage;

namespace Api.Services;

public interface IStorageManagerService
{
    Task<List<StorageDto>> GetStorageListAsync(string nodeName);
}
