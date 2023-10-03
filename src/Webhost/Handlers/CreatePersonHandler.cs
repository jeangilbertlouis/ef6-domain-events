using FluentResults;
using MediatR;
using Webhost.Db;
using Webhost.Domain;

namespace Webhost.Handlers;

public class CreatePersonHandler : IRequestHandler<CreatePersonRequest, Result>
{
    private readonly PersonContext _personContext;

    public CreatePersonHandler(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public async Task<Result> Handle(CreatePersonRequest request, CancellationToken cancellationToken)
    {
        var person = Person.Create(request.FirstName, request.LastName);
        await _personContext.Persons.AddAsync(person, cancellationToken);
        await _personContext.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}