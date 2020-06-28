using System;
using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.Common;
using CinemAPI.Models.Contracts.Projection;
using static CinemAPI.Domain.Constants.ReservationConstants;

namespace CinemAPI.Domain.Common.CancelExpiredReservations
{
    public class ProjectionValidation : ICancelExpiredReservations
    {
        private readonly ICancelExpiredReservations cancelExpiredReservations;
        private readonly IProjectionRepository projectionsRepo;

        public ProjectionValidation(ICancelExpiredReservations cancelExpiredReservations, IProjectionRepository projectionsRepo)
        {
            this.cancelExpiredReservations = cancelExpiredReservations;
            this.projectionsRepo = projectionsRepo;
        }

        public async Task Cancel(long projectionId)
        {
            IProjection projection = await projectionsRepo.GetById(projectionId);

            if (projection == null)
            {
                return;
            }

            if (projection.StartDate - TimeSpan.FromMinutes(MinutesUntilReservationExpires) > DateTime.UtcNow)
            {
                return;
            }

            if (!projection.IsReservable)
            {
                return;
            }

            await cancelExpiredReservations.Cancel(projectionId);
        }
    }
}
