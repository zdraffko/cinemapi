using CinemAPI.Domain.Contracts.DTOs;

namespace CinemAPI.Domain.Contracts.Models.TicketModels
{
    public class BuyWithReservationSummary
    {
        public BuyWithReservationSummary(string message)
        {
            this.Message = message;
            this.IsSuccessful = false;
        }

        public BuyWithReservationSummary(TicketDto ticketDto)
        {
            this.TicketDto = ticketDto;
            this.IsSuccessful = true;
        }

        public string Message { get; }

        public TicketDto TicketDto { get; }

        public bool IsSuccessful { get; }
    }
}
