
using MediatR;
using MediatrCqrs.Application.Common;
using MediatrCqrs.Application.Common.Interfaces;
using MediatrCqrs.Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace MediatrCqrs.Application.Infrastructure.Services;

public class DomainEventService : IDomainEventService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IPublisher _mediator;

    public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public Task Publish(DomainEvent domainEvent)
    {
        _logger.LogInformation("Publishing domain event by Event - {event}", domainEvent.GetType().Name);
        return _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    public static INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return Activator.CreateInstance(typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent) as INotification;
    }

}
