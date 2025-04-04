using System.Reflection;
using MediatR;

using MediatrCqrs.Application.Common.Interfaces;
using MediatrCqrs.Application.Common.Security;

namespace MediatrCqrs.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehavior(
        ICurrentUserService currentUserService
    )
    {
        _currentUserService = currentUserService;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            if (_currentUserService == null)
            {
                throw new UnauthorizedAccessException();
            }
        }

        return next();
    }
}
