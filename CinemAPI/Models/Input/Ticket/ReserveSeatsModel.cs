namespace CinemAPI.Models.Input.Ticket
{
    public class ReserveSeatsModel
    {
        public long ProjectionId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}