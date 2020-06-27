namespace CinemAPI.Models.Input.Ticket
{
    public class BuyWithoutReservationModel
    {
        public long ProjectionId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}