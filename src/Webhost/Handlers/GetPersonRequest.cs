using FluentResults;
using MediatR;
using Webhost.Contracts;

namespace Webhost.Handlers;

public record GetPersonRequest(int Id) : IRequest<Result<PersonDto>>;