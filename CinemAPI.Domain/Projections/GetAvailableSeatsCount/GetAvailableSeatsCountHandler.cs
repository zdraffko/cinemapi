using System;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts;
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

        public GetAvailableSeatsCountSummary Handle(int projectionId)
        {
            IProjection projection = projectionsRepo.GetProjectionById(projectionId);

            if (projection == null)
            {
                return new GetAvailableSeatsCountSummary($"A projection with Id {projectionId} does not exist.");
            }

            if (projection.StartDate <= DateTime.UtcNow)
            {
                return new GetAvailableSeatsCountSummary($"The projection with Id {projectionId} has already started or has finished.");
            }

            return new GetAvailableSeatsCountSummary(projection.AvailableSeatsCount);
        }
    }
}
