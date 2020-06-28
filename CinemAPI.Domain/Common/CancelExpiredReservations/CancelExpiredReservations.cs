using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.Common;

namespace CinemAPI.Domain.Common.CancelExpiredReservations
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
            int canceledReservationsCount = ticketsRepo.CancelReservationsForProjection(projectionId);
            projectionsRepo.IncreaseAvailableSeats(projectionId, canceledReservationsCount);

            projectionsRepo.ChangeReservationPolicy(projectionId, false);
        }
    }
}
