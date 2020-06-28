using System.Threading.Tasks;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Projections.NewProjection
{
    public class NewProjectionAvailableSeatsValidation : INewProjection
    {
        private readonly INewProjection newProj;

        public NewProjectionAvailableSeatsValidation(INewProjection newProj)
        {
            this.newProj = newProj;
        }
        public async Task<NewProjectionSummary> New(IProjectionCreation projection)
        {
            if (projection.AvailableSeatsCount < 0)
            {
                return new NewProjectionSummary(false, "The projection can not have less than 0 available seats.");
            }

            return await newProj.New(projection);
        }
    }
}
