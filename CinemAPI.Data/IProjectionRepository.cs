using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        Task<IProjection> GetById(long projectionId);

        Task<IProjection> Get(int movieId, int roomId, DateTime startDate);

        Task Insert(IProjectionCreation projection);

        IEnumerable<IProjection> GetActiveProjections(int roomId);

        Task DecreaseAvailableSeats(long projectionId, int seatsCount);

        Task IncreaseAvailableSeats(long projectionId, int seatsCount);

        Task ChangeReservationPolicy(long projectionId, bool isReservable);
    }
}