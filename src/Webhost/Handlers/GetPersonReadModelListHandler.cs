using System.Text.Json;
using FluentResults;
using MediatR;
using Webhost.Contracts;
using Webhost.Db;
using Webhost.Domain;

namespace Webhost.Handlers;

public class GetPersonReadModelListHandler : IRequestHandler<GetPersonReadModelListRequest, Result<List<string>>>
{
    private readonly PersonContext _personContext;

    public GetPersonReadModelListHandler(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public async Task<Result<List<string>>> Handle(GetPersonReadModelListRequest request, CancellationToken cancellationToken)
    {
        var personListAsJson = _personContext.ReadModelStores.FirstOrDefault(s=>s.Id == ReadModelStore.PersonListKey);
        if (personListAsJson == null) return Result.Fail(new Error("Person list not found"));
        var persons = JsonSerializer.Deserialize<List<PersonDto>>(personListAsJson.Value);
        return Result.Ok(persons.Select(p => $"{p.FirstName} {p.LastName}").ToList());
    }
}