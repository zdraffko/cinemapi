using CinemAPI.Domain.Contracts.DTOs;

namespace CinemAPI.Domain.Contracts.Models.TicketModels
{
    public class ReserveSeatsSummary
    {
        public ReserveSeatsSummary(string message)
        {
            this.Message = message;
            this.IsSuccessful = false;
        }

        public ReserveSeatsSummary(TicketDto ticketDto)
        {
            this.TicketDto = ticketDto;
            this.IsSuccessful = true;
        }

        public string Message { get; }

        public TicketDto TicketDto { get; }

        public bool IsSuccessful { get; }
    }
}
