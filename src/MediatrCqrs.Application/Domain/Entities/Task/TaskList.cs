using MediatrCqrs.Application.Common;
using MediatrCqrs.Application.Domain.ValueObjects.Colour;

namespace MediatrCqrs.Application.Domain.Entities.Task;

public class TaskList : AuditableEntity
{
    public Guid Id { get; set; }
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<TaskItem> Items { get; private set; } = new List<TaskItem>();

    public void AddItems(IEnumerable<TaskItem> items)
    {
        foreach (var item in items)
        {
            Items.Add(item);
        }
    }
}

