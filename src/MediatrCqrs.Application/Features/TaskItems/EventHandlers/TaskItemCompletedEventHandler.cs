using System;
using MediatR;
using MediatrCqrs.Application.Common.Models;
using MediatrCqrs.Application.Domain.Entities.Task;
using Microsoft.Extensions.Logging;

namespace MediatrCqrs.Application.Features.TaskItems.EventHandlers;

internal sealed class TaskItemCompletedEventHandler(ILogger<TaskItemCompletedEventHandler> logger) : INotificationHandler<DomainEventNotification<TaskItemCompletedEvent>>
{
    private readonly ILogger<TaskItemCompletedEventHandler> _logger = logger;

    public Task Handle(DomainEventNotification<TaskItemCompletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("VerticalSlice Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}