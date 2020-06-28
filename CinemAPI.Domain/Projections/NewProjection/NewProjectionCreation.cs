using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
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

        public async Task<NewProjectionSummary> New(IProjectionCreation projection)
        {
            await projectionsRepo.Insert(new Projection(
                projection.MovieId,
                projection.RoomId,
                projection.StartDate,
                projection.AvailableSeatsCount,
                projection.IsReservable));

            return new NewProjectionSummary(true);
        }
    }
}