﻿using Microsoft.Extensions.Logging;

namespace TPick.RHub.Infrastructure.Services;

public class HubService : IHubService
{
    private static IHubContext<HubClient> _hubContext;
    private readonly ILogger<HubService> _logger;

    public HubService(ILogger<HubService> logger, IHubContext<HubClient> hubContext)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task SendToAllAsync<TPayload>(string method, TPayload message)
    {
        _logger.LogDebug("Send to all clients!");
        await _hubContext.Clients.All.SendAsync(method, message);
    }

    public async Task SendToUserAsync<TPayload>(string userId, string method, TPayload message)
    {
        _logger.LogDebug("Send to user {UserId}!", userId);
        await _hubContext.Clients.User(userId).SendAsync(method, message);
    }

    public async Task SendToGroupAsync<TPayload>(string groupName, string method, TPayload message)
    {
        _logger.LogDebug("Send to group {GroupName}!", groupName);
        await _hubContext.Clients.Group(groupName).SendAsync(method, message);
    }
}