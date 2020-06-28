using System;
using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Projections.GetAvailableSeatsCount
{
    public class ProjectionValidation : IGetAvailableSeatsCount
    {
        private readonly IGetAvailableSeatsCount getAvailableSeatsCount;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IMovieRepository moviesRepo;

        public ProjectionValidation(
            IGetAvailableSeatsCount getAvailableSeatsCount,
            IProjectionRepository projectionsRepo,
            IMovieRepository moviesRepo)
        {
            this.getAvailableSeatsCount = getAvailableSeatsCount;
            this.projectionsRepo = projectionsRepo;
            this.moviesRepo = moviesRepo;
        }

        public async Task<GetAvailableSeatsCountSummary> Handle(long projectionId)
        {
            IProjection projection = await projectionsRepo.GetById(projectionId);

            if (projection == null)
            {
                return new GetAvailableSeatsCountSummary($"A projection with Id {projectionId} does not exist.");
            }

            IMovie movie = await moviesRepo.GetById(projection.MovieId);

            if (projection.StartDate + TimeSpan.FromMinutes(movie.DurationMinutes) <= DateTime.UtcNow)
            {
                return new GetAvailableSeatsCountSummary($"The projection with Id {projectionId} has already finished.");
            }

            if (projection.StartDate <= DateTime.UtcNow)
            {
                return new GetAvailableSeatsCountSummary($"The projection with Id {projectionId} has already started.");
            }

            return await getAvailableSeatsCount.Handle(projectionId);
        }
    }
}
