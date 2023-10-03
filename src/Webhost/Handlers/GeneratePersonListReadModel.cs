using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webhost.Contracts;
using Webhost.Db;
using Webhost.Domain;

namespace Webhost.Handlers;

public class GeneratePersonListReadModel : INotificationHandler<PersonCreatedEvent>
{
    private readonly PersonContext _personContext;

    public GeneratePersonListReadModel(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public async Task Handle(PersonCreatedEvent notification, CancellationToken cancellationToken)
    {
        var allPersons = _personContext.Persons.AsNoTracking().Where(s=>s.Id!=notification.Person.Id).ToList();
        allPersons.Add(notification.Person);
        var personListAsJson = JsonSerializer.Serialize(allPersons.Select(s=>new PersonDto(s.FirstName,s.LastName)));
        
        var existingList = _personContext.ReadModelStores.FirstOrDefault(s => s.Id == ReadModelStore.PersonListKey);
        if (existingList == null)
        {
            _personContext.ReadModelStores.Add(new ReadModelStore(ReadModelStore.PersonListKey, personListAsJson));
        }
        else
        {
            existingList.Update(personListAsJson);
            _personContext.ReadModelStores.Update(existingList);
        }

        await _personContext.SaveChangesAsync(cancellationToken);
    }
}