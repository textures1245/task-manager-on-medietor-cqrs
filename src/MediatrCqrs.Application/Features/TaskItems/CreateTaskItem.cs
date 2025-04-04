
using ErrorOr;
using FluentValidation;
using MediatR;
using MediatrCqrs.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCqrs.Application.Features.TaskItems;

public class CreateTaskItemController : ApiControllerBase
{
    [HttpPost("/api/todo-items")]
    public async Task<IActionResult> Create(CreateTaskItemCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match(id => Ok(id), Problem);
    }
}

public record CreateTaskItemCommand(Guid ListId, string? title) : IRequest<ErrorOr<Guid>>;

internal sealed class CreateTaskItemCommandValidator : AbstractValidator<CreatedTaskItemCommand>
{
    public CreateTaskItemCommandValidator()
    {
        RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
    }
}

internal sealed class CreateTaskItemHandler(ApplicationDbContext context) : IRequestHandler<CreateTaskItemCommand, ErrorOr<Guid>>
{
    private readonly ApplicationDbContext _context = context;
    public Task<ErrorOr<Guid>> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}