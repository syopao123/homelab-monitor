using System;
using Shared.Dtos;

namespace Api.Services;

public interface IResourcesManagerService
{
    Task<List<WorkloadDto>> GetResourcesAsync(string nodeName);
}
