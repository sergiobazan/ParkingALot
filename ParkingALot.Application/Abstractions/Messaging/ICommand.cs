using MediatR;
using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, ICommandBase
{ }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase
{ }

public interface ICommandBase { }
