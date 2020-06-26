using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Projections.NewProjection
{
    public class NewProjectionCreation : INewProjection
    {
        private readonly IProjectionRepository projectionsRepo;

        public NewProjectionCreation(IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
        }

        public NewProjectionSummary New(IProjectionCreation projection)
        {
            if (projection.AvailableSeatsCount < 0)
            {
                return new NewProjectionSummary(false, "The projection can not have less than 0 available seats.");
            }

            projectionsRepo.Insert(new Projection(
                projection.MovieId,
                projection.RoomId,
                projection.StartDate,
                projection.AvailableSeatsCount));

            return new NewProjectionSummary(true);
        }
    }
}