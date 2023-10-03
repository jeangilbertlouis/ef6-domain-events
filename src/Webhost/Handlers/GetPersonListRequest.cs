using FluentResults;
using MediatR;

namespace Webhost.Handlers;

public record GetPersonListRequest : IRequest<Result<List<string>>>;