namespace CleanArchitecture.Domain.Abstractions;

public abstract class Entity
{
    //? Contructor
    protected Entity() { }

    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }

    // Obtener todos los eventos como una lista
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    // Limpiar todos los eventos
    public void ClearDomainEvent()
    {
        _domainEvents.Clear();
    }

    // Disparar evento
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}