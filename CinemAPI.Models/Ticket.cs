using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Models
{
    public class Ticket : ITicket, ITicketCreation
    {
        public Ticket()
        {
        }

        public Ticket(
            long projectionId,
            int row,
            int column,
            bool isReserved,
            bool isBought)
        {
            this.ProjectionId = projectionId;
            this.Row = row;
            this.Column = column;
            this.IsReserved = isReserved;
            this.IsBought = isBought;
        }

        public long Id { get; set; }

        public long ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public bool IsReserved { get; set; }

        public bool IsBought { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
