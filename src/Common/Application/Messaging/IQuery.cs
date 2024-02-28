using MediatR;
using Shared.Results;

namespace Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}