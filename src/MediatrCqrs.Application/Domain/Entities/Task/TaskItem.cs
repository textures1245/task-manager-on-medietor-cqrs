

using MediatrCqrs.Application.Common;
using MediatrCqrs.Application.Common.Mappings;

namespace MediatrCqrs.Application.Domain.Entities.Task;

public class TaskItem : AuditableEntity, IHasDomainEvent
{
    public Guid Id { get; set; }
    public Guid ListId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && _done == false)
            {
                DomainEvents.Add(new TaskItemCompletedEvent(this)); //TODO
            }
            _done = value;
        }
    }

    public TaskList List { get; set; } = null;

    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}

public enum PriorityLevel
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3,
}