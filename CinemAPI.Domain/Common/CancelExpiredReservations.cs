using System;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.Common;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Common
{
    public class CancelExpiredReservations : ICancelExpiredReservations
    {
        private readonly ITicketRepository ticketsRepo;
        private readonly IProjectionRepository projectionsRepo;

        public CancelExpiredReservations(ITicketRepository ticketsRepo, IProjectionRepository projectionsRepo)
        {
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
        }

        public void Cancel(long projectionId)
        {
            IProjection projection = projectionsRepo.GetById(projectionId);

            if (projection == null)
            {
                return;
            }

            if (projection.StartDate - TimeSpan.FromMinutes(10) > DateTime.UtcNow)
            {
                return;
            }

            if (!projection.IsReservable)
            {
                return;
            }

            int canceledReservationsCount = ticketsRepo.CancelReservationsForProjection(projectionId);
            projectionsRepo.IncreaseAvailableSeats(projectionId, canceledReservationsCount);
            projectionsRepo.ChangeReservationPolicy(projectionId, false);
        }
    }
}
