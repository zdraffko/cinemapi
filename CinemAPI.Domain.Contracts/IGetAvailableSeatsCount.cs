using CinemAPI.Domain.Contracts.Models;

namespace CinemAPI.Domain.Contracts
{
    public interface IGetAvailableSeatsCount
    {
        GetAvailableSeatsCountSummary Handle(int projectionId);
    }
}
