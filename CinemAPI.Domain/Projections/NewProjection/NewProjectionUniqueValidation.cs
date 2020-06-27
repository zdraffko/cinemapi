﻿using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Projections.NewProjection
{
    public class NewProjectionUniqueValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly INewProjection newProj;

        public NewProjectionUniqueValidation(IProjectionRepository projectRepo, INewProjection newProj)
        {
            this.projectRepo = projectRepo;
            this.newProj = newProj;
        }

        public NewProjectionSummary New(IProjectionCreation proj)
        {
            IProjection projection = projectRepo.Get(proj.MovieId, proj.RoomId, proj.StartDate);

            if (projection != null)
            {
                return new NewProjectionSummary(false, "Projection already exists");
            }

            return newProj.New(proj);
        }
    }
}