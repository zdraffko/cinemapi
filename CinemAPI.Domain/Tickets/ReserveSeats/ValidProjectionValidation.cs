using System;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Tickets.ReserveSeats
{
    public class ValidProjectionValidation : IReserveSeats
    {
        private readonly IReserveSeats reserveSeats;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IMovieRepository moviesRepo;


        public ValidProjectionValidation(
            IReserveSeats reserveSeats,
            IProjectionRepository projectionsRepo,
            IMovieRepository moviesRepo)
        {
            this.reserveSeats = reserveSeats;
            this.projectionsRepo = projectionsRepo;
            this.moviesRepo = moviesRepo;
        }

        public ReserveSeatsSummary Handle(long projectionId, int row, int column)
        {
            IProjection projection = projectionsRepo.GetById(projectionId);

            if (projection == null)
            {
                return new ReserveSeatsSummary($"A projection with Id {projectionId} does not exist.");
            }

            IMovie movie = moviesRepo.GetById(projection.MovieId);

            if (projection.StartDate + TimeSpan.FromMinutes(movie.DurationMinutes) <= DateTime.UtcNow)
            {
                return new ReserveSeatsSummary($"The projection with Id {projectionId} has already finished.");
            }

            if (projection.StartDate <= DateTime.UtcNow)
            {
                return new ReserveSeatsSummary($"The projection with Id {projectionId} has already started.");
            }

            if (projection.StartDate.AddMinutes(-10) <= DateTime.UtcNow)
            {
                return new ReserveSeatsSummary($"The projection with Id {projectionId} is starting in less than 10 minutes.");
            }

            return reserveSeats.Handle(projectionId, row, column);
        }
    }
}
