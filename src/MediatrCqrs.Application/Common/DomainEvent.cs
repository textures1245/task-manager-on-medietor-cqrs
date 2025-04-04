namespace MediatrCqrs.Application.Common;

public abstract class DomainEvent
{

    protected DomainEvent()
    {
        DataOccurredOn = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }
    public DateTimeOffset DataOccurredOn { get; protected set; } = DateTimeOffset.UtcNow;
}

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; }
}