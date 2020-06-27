using System;

namespace CinemAPI.Domain.Contracts.DTOs
{
    public class TicketDto
    {
        public long Id { get; set; }

        public DateTime ProjectionStartDate { get; set; }

        public string MovieName { get; set; }

        public string CinemaName { get; set; }

        public int RoomNumber { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
