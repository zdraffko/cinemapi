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

        public ReservedSeatsValidation(
            IReserveSeats reserveSeats,
            ITicketRepository ticketsRepo)
        {
            this.reserveSeats = reserveSeats;
            this.ticketsRepo = ticketsRepo;
        }

        public ReserveSeatsSummary Handle(long projectionId, int row, int column)
        {
            ITicket ticket = ticketsRepo.Get(projectionId, row, column);

            if (ticket != null && ticket.IsReserved)
            {
                return new ReserveSeatsSummary($"The seat at row {row} and column {column} is already reserved.");
            }

            return reserveSeats.Handle(projectionId, row, column);
        }
    }
}
