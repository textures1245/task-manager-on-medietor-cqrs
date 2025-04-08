using System;
using ErrorOr;
using MediatR;
using MediatrCqrs.Application.Common;
using MediatrCqrs.Application.Domain.Entities.Task;
using MediatrCqrs.Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCqrs.Application.Features;

public class DeleteTaskItemController : ApiControllerBase

{
    [HttpDelete("/api/task-items/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteTaskItemCommand(id));
        return result.Match(_ => NoContent(), Problem);
    }
}

public record DeleteTaskItemCommand(int Id) : IRequest<ErrorOr<Success>>;

internal sealed class DeleteTodoItemCOmmandHandler(ApplicationDbContext context) : IRequestHandler<DeleteTaskItemCommand, ErrorOr<Success>>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ErrorOr<Success>> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
    {
        var taskItem = await _context.TaskItems.FindAsync([request.Id], cancellationToken);

        if (taskItem is null)
        {
            return Error.NotFound(description: "Todo item not found.");
        }

        _context.TaskItems.Remove(taskItem);

        taskItem.DomainEvents.Add(new TaskItemDeletedEvent(taskItem));

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
