using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.Common;
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
        private readonly ICancelExpiredReservations cancelExpiredReservations;

        public SeatsValidation(
            IBuyWithoutReservation buyWithoutReservation,
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo,
            IRoomRepository roomsRepo,
            ICancelExpiredReservations cancelExpiredReservations)
        {
            this.buyWithoutReservation = buyWithoutReservation;
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
            this.roomsRepo = roomsRepo;
            this.cancelExpiredReservations = cancelExpiredReservations;
        }

        public async Task<BuyWithoutReservationSummary> Handle(long projectionId, int row, int column)
        {
            ITicket ticket = await ticketsRepo.Get(projectionId, row, column);
            IProjection projection = await projectionsRepo.GetById(projectionId);
            IRoom room = await roomsRepo.GetById(projection.RoomId);

            if (row > room.Rows || row < 0)
            {
                return new BuyWithoutReservationSummary($"The room does not have a number {row} row.");
            }

            if (column > room.SeatsPerRow || column < 0)
            {
                return new BuyWithoutReservationSummary($"The room does not have a number {column} column.");
            }

            await cancelExpiredReservations.Cancel(projectionId);

            if (ticket != null && ticket.IsBought)
            {
                return new BuyWithoutReservationSummary($"The seat at row {row} and column {column} is already bought.");
            }

            if (ticket != null && ticket.IsReserved)
            {
                return new BuyWithoutReservationSummary($"The seat at row {row} and column {column} is already reserved.");
            }

            return await buyWithoutReservation.Handle(projectionId, row, column);
        }
    }
}
