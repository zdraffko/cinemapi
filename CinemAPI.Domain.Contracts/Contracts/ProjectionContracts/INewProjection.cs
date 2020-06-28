using System.Threading.Tasks;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Contracts.Contracts.ProjectionContracts
{
    public interface INewProjection
    {
        Task<NewProjectionSummary> New(IProjectionCreation projection);
    }
}