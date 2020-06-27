using System;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Tickets.BuyWithoutReservation
{
    public class ProjectionValidation : IBuyWithoutReservation
    {
        private readonly IBuyWithoutReservation buyWithoutReservation;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IMovieRepository moviesRepo;

        public ProjectionValidation(
            IBuyWithoutReservation buyWithoutReservation,
            IProjectionRepository projectionsRepo,
            IMovieRepository moviesRepo)
        {
            this.buyWithoutReservation = buyWithoutReservation;
            this.projectionsRepo = projectionsRepo;
            this.moviesRepo = moviesRepo;
        }

        public BuyWithoutReservationSummary Handle(long projectionId, int row, int column)
        {
            IProjection projection = projectionsRepo.GetById(projectionId);

            if (projection == null)
            {
                return new BuyWithoutReservationSummary($"A projection with Id {projectionId} does not exist.");
            }

            IMovie movie = moviesRepo.GetById(projection.MovieId);

            if (projection.StartDate + TimeSpan.FromMinutes(movie.DurationMinutes) <= DateTime.UtcNow)
            {
                return new BuyWithoutReservationSummary($"The projection with Id {projectionId} has already finished.");
            }

            if (projection.StartDate <= DateTime.UtcNow)
            {
                return new BuyWithoutReservationSummary($"The projection with Id {projectionId} has already started.");
            }
            return buyWithoutReservation.Handle(projectionId, row, column);
        }
    }
}
