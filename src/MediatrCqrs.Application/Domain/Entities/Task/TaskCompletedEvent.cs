using MediatrCqrs.Application.Common;

namespace MediatrCqrs.Application.Domain.Entities.Task;

internal sealed class TaskItemCompletedEvent(TaskItem item) : DomainEvent
{
    public TaskItem Item { get; } = item;
}
