using MediatR;
using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }