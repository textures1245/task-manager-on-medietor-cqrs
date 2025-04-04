using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

using MediatrCqrs.Application.Common.Interfaces;

namespace MediatrCqrs.Application.Common.Behaviors;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;

        return Task.Run(
            () => _logger.LogInformation(
                "MediatorCqrs Requests: {Name} {@UserId} {@Request}",
                requestName,
                userId,
                request),
                cancellationToken);
    }
}
