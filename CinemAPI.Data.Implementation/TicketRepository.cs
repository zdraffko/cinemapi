using System;
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

        public ITicket Get(DateTime projectionStartDate, string movieName, string cinemaName, int roomNumber, int row, int column)
        {
            return db.Tickets.FirstOrDefault(t =>
                t.ProjectionStartDate == projectionStartDate &&
                t.MovieName == movieName &&
                t.CinemaName == cinemaName &&
                t.RoomNumber == roomNumber &&
                t.Row == row &&
                t.Column == column);
        }

        public void Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(
                ticket.ProjectionStartDate,
                ticket.MovieName,
                ticket.CinemaName,
                ticket.RoomNumber,
                ticket.Row,
                ticket.Column,
                ticket.IsReserved,
                ticket.IsBought
                );

            db.Tickets.Add(newTicket);
            db.SaveChanges();
        }
    }
}
