using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.DTOs;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<ITicket> GetById(long ticketId)
        {
            return await db.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task<ITicket> Get(long projectionId, int row, int column)
        {
            return await db.Tickets.FirstOrDefaultAsync(t =>
                t.ProjectionId == projectionId &&
                t.Row == row &&
                t.Column == column);
        }

        public async Task Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(
                ticket.ProjectionId,
                ticket.Row,
                ticket.Column,
                ticket.IsReserved,
                ticket.IsBought
                );

            db.Tickets.Add(newTicket);
            await db.SaveChangesAsync();
        }

        public async Task ReserveSeats(long projectionId, int row, int column)
        {
            Ticket ticket = await this.Get(projectionId, row, column) as Ticket;

            if (ticket == null)
            {
                await this.Insert(new Ticket(
                    projectionId,
                    row,
                    column,
                    true,
                    false)
                );
            }
            else
            {
                ticket.IsReserved = true;
            }

            await db.SaveChangesAsync();
        }

        public async Task BuyWithoutReservation(long projectionId, int row, int column)
        {
            Ticket ticket = await this.Get(projectionId, row, column) as Ticket;

            if (ticket == null)
            {
                await this.Insert(new Ticket(
                    projectionId,
                    row,
                    column,
                    false,
                    true)
                );
            }
            else
            {
                ticket.IsBought = true;
            }

            await db.SaveChangesAsync();
        }

        public async Task BuyWithReservation(long ticketId)
        {
            Ticket ticket = await this.GetById(ticketId) as Ticket;

            if (ticket == null)
            {
                return;
            }

            ticket.IsBought = true;

            await db.SaveChangesAsync();
        }

        public async Task CancelReservation(long ticketId)
        {
            Ticket ticket = await this.GetById(ticketId) as Ticket;

            if (ticket == null)
            {
                return;
            }

            ticket.IsReserved = false;

            await db.SaveChangesAsync();
        }

        public async Task<int> CancelReservationsForProjection(long projectionId)
        {
            IQueryable<Ticket> tickets = db.Tickets.Where(t => t.ProjectionId == projectionId);

            int canceledReservationsCount = 0;
            foreach (Ticket ticket in tickets)
            {
                if (ticket.IsReserved && !ticket.IsBought)
                {
                    ticket.IsReserved = false;
                    canceledReservationsCount += 1;
                }
            }

            await db.SaveChangesAsync();

            return canceledReservationsCount;
        }

        public async Task<FullTicketInfoDto> GetFullTicketInformation(long ticketId)
        {
            return await db.Tickets
                .Where(t => t.Id == ticketId)
                .Select(t => new FullTicketInfoDto()
                {
                    Id = t.Id,
                    ProjectionStartDate = t.Projection.StartDate,
                    MovieName = t.Projection.Movie.Name,
                    CinemaName = t.Projection.Room.Cinema.Name,
                    RoomNumber = t.Projection.Room.Number,
                    Row = t.Row,
                    Column = t.Column
                })
                .FirstOrDefaultAsync();
        }
    }
}
