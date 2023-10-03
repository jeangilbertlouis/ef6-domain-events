using Webhost.Domain;

namespace Webhost.Handlers;

public class PersonCreatedEvent : IDomainEvent
{
    public Person Person { get; set; }
}