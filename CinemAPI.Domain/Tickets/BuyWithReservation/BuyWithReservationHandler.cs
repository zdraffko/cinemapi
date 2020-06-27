using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.DTOs;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.Tickets.BuyWithReservation
{
    public class BuyWithReservationHandler : IBuyWithReservation
    {
        private readonly ITicketRepository ticketsRepo;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IMovieRepository moviesRepo;
        private readonly IRoomRepository roomsRepo;
        private readonly ICinemaRepository cinemasRepo;

        public BuyWithReservationHandler(
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo,
            IMovieRepository moviesRepo,
            IRoomRepository roomsRepo,
            ICinemaRepository cinemasRepo)
        {
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
            this.moviesRepo = moviesRepo;
            this.roomsRepo = roomsRepo;
            this.cinemasRepo = cinemasRepo;
        }

        public BuyWithReservationSummary Handle(long ticketId)
        {
            ticketsRepo.BuyWithReservation(ticketId);

            ITicket ticket = ticketsRepo.GetById(ticketId);

            projectionsRepo.DecreaseAvailableSeats(ticket.ProjectionId, 1);

            IProjection projection = projectionsRepo.GetById(ticket.ProjectionId);
            IMovie movie = moviesRepo.GetById(projection.MovieId);
            IRoom room = roomsRepo.GetById(projection.RoomId);
            ICinema cinema = cinemasRepo.GetById(room.CinemaId);

            return new BuyWithReservationSummary(new TicketDto
            {
                Id = ticketId,
                ProjectionStartDate = projection.StartDate,
                MovieName = movie.Name,
                CinemaName = cinema.Name,
                RoomNumber = room.Number,
                Row = ticket.Row,
                Column = ticket.Column
            });
        }
    }
}
