using Webhost.Handlers;

namespace Webhost.Domain;

public class Person
{
    private Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    private Person()
    {
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Id { get; private set; }

    public override string ToString()
    {
        return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Id)}: {Id}";
    }

    public static Person Create(string firstName, string lastName)
    {
        var person =  new Person(firstName, lastName);
        person.QueueDomainEvent(new PersonCreatedEvent{Person = person});
        return person;
    }
    
    public List<IDomainEvent> DomainEvents { get; } = new List<IDomainEvent>();

    public void QueueDomainEvent(IDomainEvent @event)
    {
        DomainEvents.Add(@event);
    }
}