using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection GetById(long projectionId);

        IProjection Get(int movieId, int roomId, DateTime startDate);

        void Insert(IProjectionCreation projection);

        IEnumerable<IProjection> GetActiveProjections(int roomId);

        void DecreaseAvailableSeats(long projectionId, int seatsCount);

        void IncreaseAvailableSeats(long projectionId, int seatsCount);

        void ChangeReservationPolicy(long projectionId, bool isReservable);
    }
}