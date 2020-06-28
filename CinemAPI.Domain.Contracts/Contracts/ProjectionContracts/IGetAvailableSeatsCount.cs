using System.Threading.Tasks;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;

namespace CinemAPI.Domain.Contracts.Contracts.ProjectionContracts
{
    public interface IGetAvailableSeatsCount
    {
        Task<GetAvailableSeatsCountSummary> Handle(long projectionId);
    }
}
