using CinemAPI.Domain.Contracts.Models.ProjectionModels;

namespace CinemAPI.Domain.Contracts.Contracts
{
    public interface IGetAvailableSeatsCount
    {
        GetAvailableSeatsCountSummary Handle(int projectionId);
    }
}
