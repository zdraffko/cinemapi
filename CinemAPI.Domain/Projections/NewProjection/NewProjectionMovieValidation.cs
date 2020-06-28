﻿using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Projections.NewProjection
{
    public class NewProjectionMovieValidation : INewProjection
    {
        private readonly IMovieRepository movieRepo;
        private readonly INewProjection newProj;

        public NewProjectionMovieValidation(IMovieRepository movieRepo, INewProjection newProj)
        {
            this.movieRepo = movieRepo;
            this.newProj = newProj;
        }

        public async Task<NewProjectionSummary> New(IProjectionCreation projection)
        {
            IMovie movie = await movieRepo.GetById(projection.MovieId);

            if (movie == null)
            {
                return new NewProjectionSummary(false, $"Movie with id {projection.MovieId} does not exist");
            }

            return await newProj.New(projection);
        }
    }
}