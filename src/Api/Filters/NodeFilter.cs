using System;
using Api.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class NodeFilter : IAsyncActionFilter
{
    private readonly INodeManagerService _nodeManager;
    private readonly NodeContext _nodeContext;

    public NodeFilter(INodeManagerService nodeManager, NodeContext nodeContext)
    {
        _nodeManager = nodeManager;
        _nodeContext = nodeContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Get active node
        var activeNode = await _nodeManager.GetActiveNodeNameAsync();
        // Set node context property
        _nodeContext.ActiveNodeName = activeNode;
        await next();
    }
}
