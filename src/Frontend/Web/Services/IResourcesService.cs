using System;
using Shared.Dtos;

namespace Web.Services;

public interface IResourcesService
{

    Task<List<WorkloadDto>> GetResourcesAsync(string nodeName);

}
