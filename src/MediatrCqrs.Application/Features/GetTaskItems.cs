using System;
using ErrorOr;
using FluentValidation;
using MediatR;
using MediatrCqrs.Application.Common;
using MediatrCqrs.Application.Common.Mappings;
using MediatrCqrs.Application.Common.Models;
using MediatrCqrs.Application.Domain.Entities.Task;
using MediatrCqrs.Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCqrs.Application.Features;

public class GetTaskItemsController : ApiControllerBase
{
    [HttpGet("/api/todo-items")]
    public async Task<IActionResult> GetTaskItems([FromQuery] GetTaskItemsQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match(
            Ok,
            Problem);
    }
}

public record TodoItemBriefResponse(Guid Id, Guid ListId, string? Title, bool Done);

public record GetTaskItemsQuery(Guid ListId, int PageNumber = 1, int PageSize = 10) : IRequest<ErrorOr<PaginatedList<TodoItemBriefResponse>>>;

internal sealed class GetTaskItemsQueryValidator : AbstractValidator<GetTaskItemsQuery>
{
    public GetTaskItemsQueryValidator()
    {
        RuleFor(x => x.ListId)
            .NotEmpty().WithMessage("ListId is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetTaskItemsQueryHandler(ApplicationDbContext context) : IRequestHandler<GetTaskItemsQuery, ErrorOr<PaginatedList<TodoItemBriefResponse>>>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ErrorOr<PaginatedList<TodoItemBriefResponse>>> Handle(GetTaskItemsQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await _context.TaskItems
            .Where(item => item.ListId == request.ListId)
            .OrderBy(item => item.Title)
            .Select(item => ToDto(item))
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return paginatedList;
    }

    private static TodoItemBriefResponse ToDto(TaskItem todoItem) =>
        new(todoItem.Id, todoItem.ListId, todoItem.Title, todoItem.Done);
}
