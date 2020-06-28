using System;
using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Projections.GetAvailableSeatsCount
{
    public class GetAvailableSeatsCountHandler : IGetAvailableSeatsCount
    {
        private readonly IProjectionRepository projectionsRepo;

        public GetAvailableSeatsCountHandler(IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
        }

        public async Task<GetAvailableSeatsCountSummary> Handle(long projectionId)
        {
            IProjection projection = await projectionsRepo.GetById(projectionId);

            return new GetAvailableSeatsCountSummary(projection.AvailableSeatsCount);
        }
    }
}
