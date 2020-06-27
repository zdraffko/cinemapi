﻿using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Contracts.Contracts.ProjectionContracts
{
    public interface INewProjection
    {
        NewProjectionSummary New(IProjectionCreation projection);
    }
}