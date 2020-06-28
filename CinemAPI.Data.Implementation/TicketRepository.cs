using System.Linq;
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

        public ITicket GetById(long ticketId)
        {
            return db.Tickets.FirstOrDefault(t => t.Id == ticketId);
        }

        public ITicket Get(long projectionId, int row, int column)
        {
            return db.Tickets.FirstOrDefault(t =>
                t.ProjectionId == projectionId &&
                t.Row == row &&
                t.Column == column);
        }

        public void Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(
                ticket.ProjectionId,
                ticket.Row,
                ticket.Column,
                ticket.IsReserved,
                ticket.IsBought
                );

            db.Tickets.Add(newTicket);
            db.SaveChanges();
        }

        public void ReserveSeats(long projectionId, int row, int column)
        {
            Ticket ticket = this.Get(projectionId, row, column) as Ticket;

            if (ticket == null)
            {
                this.Insert(new Ticket(
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

            db.SaveChanges();
        }

        public void BuyWithoutReservation(long projectionId, int row, int column)
        {
            Ticket ticket = this.Get(projectionId, row, column) as Ticket;

            if (ticket == null)
            {
                this.Insert(new Ticket(
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

            db.SaveChanges();
        }

        public void BuyWithReservation(long ticketId)
        {
            Ticket ticket = this.GetById(ticketId) as Ticket;

            if (ticket == null)
            {
                return;
            }

            ticket.IsBought = true;

            db.SaveChanges();
        }

        public void CancelReservation(long ticketId)
        {
            Ticket ticket = this.GetById(ticketId) as Ticket;

            if (ticket == null)
            {
                return;
            }

            ticket.IsReserved = false;

            db.SaveChanges();
        }

        public int CancelReservationsForProjection(long projectionId)
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

            db.SaveChanges();

            return canceledReservationsCount;
        }

        public FullTicketInfoDto GetFullTicketInformation(long ticketId)
        {
            return db.Tickets
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
                .FirstOrDefault();
        }
    }
}
