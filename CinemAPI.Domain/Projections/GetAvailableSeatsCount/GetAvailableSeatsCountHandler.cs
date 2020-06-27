using System;
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

        public GetAvailableSeatsCountSummary Handle(long projectionId)
        {
            IProjection projection = projectionsRepo.GetById(projectionId);

            return new GetAvailableSeatsCountSummary(projection.AvailableSeatsCount);
        }
    }
}
