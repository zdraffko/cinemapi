namespace CinemAPI.Domain.Contracts.Models
{
    public class GetAvailableSeatsCountSummary
    {
        public GetAvailableSeatsCountSummary(string message)
        {
            this.Message = message;
            this.IsValid = false;
        }

        public GetAvailableSeatsCountSummary(int availableSeats)
        {
            this.AvailableSeats = availableSeats;
            this.IsValid = true;
        }

        public string Message { get; }

        public int AvailableSeats { get; }

        public bool IsValid { get; }
    }
}
