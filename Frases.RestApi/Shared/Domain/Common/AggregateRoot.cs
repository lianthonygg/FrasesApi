using FrasesApi.Shared.Domain.Interfaces;

namespace FrasesApi.Shared.Domain.Common;

public class AggregateRoot<TId>(TId id) : Entity<TId>(id)
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}