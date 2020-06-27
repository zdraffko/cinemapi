using System.Linq;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;

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

        public void CancelReservation(long ticketId)
        {
            Ticket ticket = db.Tickets.FirstOrDefault(t => t.Id == ticketId);

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
    }
}
