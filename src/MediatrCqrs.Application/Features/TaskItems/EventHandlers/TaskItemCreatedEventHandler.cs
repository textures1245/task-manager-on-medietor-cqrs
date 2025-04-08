using MediatR;
using MediatrCqrs.Application.Common.Models;
using MediatrCqrs.Application.Domain.Entities.Task;
using Microsoft.Extensions.Logging;

namespace MediatrCqrs.Application.Features.TaskItems.EventHandlers;

public class TaskItemCreatedEventHandler(ILogger<TaskItemCreatedEventHandler> logger) : INotificationHandler<DomainEventNotification<TaskItemCreatedEvent>>
{
    private readonly ILogger<TaskItemCreatedEventHandler> _logger = logger;

    Task INotificationHandler<DomainEventNotification<TaskItemCreatedEvent>>.Handle(DomainEventNotification<TaskItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("MediatorCqrs Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
