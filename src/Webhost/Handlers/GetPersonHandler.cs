using FluentResults;
using MediatR;
using Webhost.Contracts;
using Webhost.Db;

namespace Webhost.Handlers;

public class GetPersonHandler : IRequestHandler<GetPersonRequest, Result<PersonDto>>
{
    private readonly PersonContext _personContext;

    public GetPersonHandler(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public async Task<Result<PersonDto>> Handle(GetPersonRequest request, CancellationToken cancellationToken)
    {
        var person = _personContext.Persons.FirstOrDefault(s => s.Id == request.Id);
        if (person == null) return Result.Fail(new Error($"Person with id {request.Id} not found"));
        return Result.Ok(new PersonDto(person.FirstName, person.LastName));
    }
}