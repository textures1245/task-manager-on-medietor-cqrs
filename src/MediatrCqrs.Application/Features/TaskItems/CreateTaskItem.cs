
using ErrorOr;
using FluentValidation;
using MediatR;
using MediatrCqrs.Application.Common;
using MediatrCqrs.Application.Domain.Entities.Task;
using MediatrCqrs.Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCqrs.Application.Features.TaskItems;

public class CreateTaskItemController : ApiControllerBase
{
    [HttpPost("/api/task-items")]
    public async Task<IActionResult> Create(CreateTaskItemCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match(id => Ok(id), Problem);
    }
}

public record CreateTaskItemCommand(Guid ListId, string? Title) : IRequest<ErrorOr<Guid>>;

internal sealed class CreateTaskItemCommandValidator : AbstractValidator<CreateTaskItemCommand>
{
    public CreateTaskItemCommandValidator()
    {
        RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
    }
}

internal sealed class CreateTaskItemCommandHandler(ApplicationDbContext context) : IRequestHandler<CreateTaskItemCommand, ErrorOr<Guid>>
{
    private readonly ApplicationDbContext _context = context;
    public async Task<ErrorOr<Guid>> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TaskItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        entity.DomainEvents.Add(new TaskItemCreatedEvent(entity));

        _context.TaskItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;


    }
}