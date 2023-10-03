using FluentResults;
using MediatR;
using Webhost.Db;

namespace Webhost.Handlers;

public class GetPersonListHandler : IRequestHandler<GetPersonListRequest, Result<List<string>>>
{
    private readonly PersonContext _personContext;

    public GetPersonListHandler(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public async Task<Result<List<string>>> Handle(GetPersonListRequest request, CancellationToken cancellationToken)
    {
        var persons = _personContext.Persons.ToList();
        return Result.Ok(persons.Select(p => $"{p.FirstName} {p.LastName}").ToList());
    }
}