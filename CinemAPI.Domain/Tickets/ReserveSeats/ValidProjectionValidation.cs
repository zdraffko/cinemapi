using System;
using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using static CinemAPI.Domain.Constants.ReservationConstants;

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

        public async Task<ReserveSeatsSummary> Handle(long projectionId, int row, int column)
        {
            IProjection projection = await projectionsRepo.GetById(projectionId);

            if (projection == null)
            {
                return new ReserveSeatsSummary($"A projection with Id {projectionId} does not exist.");
            }

            IMovie movie = await moviesRepo.GetById(projection.MovieId);

            if (projection.StartDate + TimeSpan.FromMinutes(movie.DurationMinutes) <= DateTime.UtcNow)
            {
                return new ReserveSeatsSummary($"The projection with Id {projectionId} has already finished.");
            }

            if (projection.StartDate <= DateTime.UtcNow)
            {
                return new ReserveSeatsSummary($"The projection with Id {projectionId} has already started.");
            }

            if (projection.StartDate - TimeSpan.FromMinutes(MinutesUntilReservationExpires) <= DateTime.UtcNow)
            {
                return new ReserveSeatsSummary($"The projection with Id {projectionId} is starting in less than 10 minutes.");
            }

            return await reserveSeats.Handle(projectionId, row, column);
        }
    }
}
