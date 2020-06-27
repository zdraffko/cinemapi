using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.Tickets.BuyWithoutReservation
{
    public class SeatsValidation : IBuyWithoutReservation
    {
        private readonly IBuyWithoutReservation buyWithoutReservation;
        private readonly ITicketRepository ticketsRepo;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IRoomRepository roomsRepo;

        public SeatsValidation(
            IBuyWithoutReservation buyWithoutReservation,
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo,
            IRoomRepository roomsRepo)
        {
            this.buyWithoutReservation = buyWithoutReservation;
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
            this.roomsRepo = roomsRepo;
        }

        public BuyWithoutReservationSummary Handle(long projectionId, int row, int column)
        {
            ITicket ticket = ticketsRepo.Get(projectionId, row, column);
            IProjection projection = projectionsRepo.GetById(projectionId);
            IRoom room = roomsRepo.GetById(projection.RoomId);

            if (row > room.Rows || row < 0)
            {
                return new BuyWithoutReservationSummary($"The room does not have a number {row} row.");
            }

            if (column > room.SeatsPerRow || column < 0)
            {
                return new BuyWithoutReservationSummary($"The room does not have a number {column} column.");
            }

            if (ticket != null && ticket.IsBought)
            {
                return new BuyWithoutReservationSummary($"The seat at row {row} and column {column} is already bought.");
            }

            if (ticket != null && ticket.IsReserved)
            {
                return new BuyWithoutReservationSummary($"The seat at row {row} and column {column} is already reserved.");
            }

            return buyWithoutReservation.Handle(projectionId, row, column);
        }
    }
}
