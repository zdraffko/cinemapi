using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Room;

namespace CinemAPI.Domain.Tickets.ReserveSeats
{
    public class ExistingSeatsValidation : IReserveSeats
    {
        private readonly IReserveSeats reserveSeats;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IRoomRepository roomsRepo;


        public ExistingSeatsValidation(
            IReserveSeats reserveSeats,
            IProjectionRepository projectionsRepo,
            IRoomRepository roomsRepo)
        {
            this.reserveSeats = reserveSeats;
            this.projectionsRepo = projectionsRepo;
            this.roomsRepo = roomsRepo;
        }

        public ReserveSeatsSummary Handle(long projectionId, int row, int column)
        {
            IProjection projection = projectionsRepo.GetById(projectionId);
            IRoom room = roomsRepo.GetById(projection.RoomId);

            if (row > room.Rows || row < 0)
            {
                return new ReserveSeatsSummary($"The room does not have a number {row} row.");
            }

            if (column > room.SeatsPerRow || column < 0)
            {
                return new ReserveSeatsSummary($"The room does not have a number {column} column.");
            }

            return reserveSeats.Handle(projectionId, row, column);
        }
    }
}
