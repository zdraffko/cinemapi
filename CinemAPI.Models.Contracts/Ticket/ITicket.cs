using System;

namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicket
    {
        long Id { get; set; }

        DateTime ProjectionStartDate { get; set; }

        string MovieName { get; set; }

        string CinemaName { get; set; }

        int RoomNumber { get; set; }

        int Row { get; set; }

        int Column { get; set; }

        bool IsReserved { get; set; }

        bool IsBought { get; set; }
    }
}
