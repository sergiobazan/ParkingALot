using MediatR;
using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }
