using System.Threading.Tasks;
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

        public async Task Cancel(long projectionId)
        {
            int canceledReservationsCount = await ticketsRepo.CancelReservationsForProjection(projectionId);
            await projectionsRepo.IncreaseAvailableSeats(projectionId, canceledReservationsCount);

            await projectionsRepo.ChangeReservationPolicy(projectionId, false);
        }
    }
}
