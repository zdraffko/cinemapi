using CinemAPI.Domain.Contracts.Models.ProjectionModels;

namespace CinemAPI.Domain.Contracts.Contracts.ProjectionContracts
{
    public interface IGetAvailableSeatsCount
    {
        GetAvailableSeatsCountSummary Handle(long projectionId);
    }
}
