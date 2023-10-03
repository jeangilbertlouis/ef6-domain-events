using FluentResults;
using MediatR;

namespace Webhost.Handlers;

public record GetPersonReadModelListRequest : IRequest<Result<List<string>>>;