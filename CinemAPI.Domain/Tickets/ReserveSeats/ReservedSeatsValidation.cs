using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.Tickets.ReserveSeats
{
    public class ReservedSeatsValidation : IReserveSeats
    {
        private readonly IReserveSeats reserveSeats;
        private readonly ITicketRepository ticketsRepo;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IMovieRepository moviesRepo;
        private readonly IRoomRepository roomsRepo;
        private readonly ICinemaRepository cinemasRepo;

        public ReservedSeatsValidation(
            IReserveSeats reserveSeats,
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo,
            IMovieRepository moviesRepo,
            IRoomRepository roomsRepo,
            ICinemaRepository cinemasRepo)
        {
            this.reserveSeats = reserveSeats;
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
            this.moviesRepo = moviesRepo;
            this.roomsRepo = roomsRepo;
            this.cinemasRepo = cinemasRepo;
        }

        public ReserveSeatsSummary Handle(long projectionId, int row, int column)
        {
            IProjection projection = projectionsRepo.GetById(projectionId);
            IMovie movie = moviesRepo.GetById(projection.MovieId);
            IRoom room = roomsRepo.GetById(projection.RoomId);
            ICinema cinema = cinemasRepo.GetById(room.CinemaId);

            ITicket ticket = ticketsRepo.Get(
                projection.StartDate,
                movie.Name,
                cinema.Name,
                room.Number,
                row,
                column
            );

            if (ticket != null && ticket.IsReserved)
            {
                return new ReserveSeatsSummary($"The seat at row {row} and column {column} is already reserved.");
            }

            return reserveSeats.Handle(projectionId, row, column);
        }
    }
}
