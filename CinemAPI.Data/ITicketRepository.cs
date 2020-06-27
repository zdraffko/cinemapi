using System;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        ITicket GetById(long ticketId);

        ITicket Get(
            DateTime projectionStartDate,
            string movieName,
            string cinemaName,
            int roomNumber,
            int row,
            int column);

        void Insert(ITicketCreation ticket);
    }
}
