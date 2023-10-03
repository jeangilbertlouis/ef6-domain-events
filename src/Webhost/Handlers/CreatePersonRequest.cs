using FluentResults;
using MediatR;

namespace Webhost.Handlers;

public record CreatePersonRequest(string FirstName, string LastName) : IRequest<Result>;