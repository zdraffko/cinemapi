using System;
using System.Collections.Generic;
using System.Linq;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.Projections.NewProjection
{
    public class NewProjectionPreviousOverlapValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly IMovieRepository movieRepo;
        private readonly INewProjection newProj;

        public NewProjectionPreviousOverlapValidation(IProjectionRepository projectRepo, IMovieRepository movieRepo, INewProjection proj)
        {
            this.projectRepo = projectRepo;
            this.movieRepo = movieRepo;
            this.newProj = proj;
        }

        public NewProjectionSummary New(IProjectionCreation proj)
        {
            IEnumerable<IProjection> movieProjectionsInRoom = projectRepo.GetActiveProjections(proj.RoomId);

            IProjection previousProjection = movieProjectionsInRoom.Where(x => x.StartDate < proj.StartDate)
                                                                        .OrderByDescending(x => x.StartDate)
                                                                        .FirstOrDefault();

            if (previousProjection != null)
            {
                IMovie previousProjectionMovie = movieRepo.GetById(previousProjection.MovieId);

                DateTime previousProjectionEnd = previousProjection.StartDate.AddMinutes(previousProjectionMovie.DurationMinutes);

                if (previousProjectionEnd >= proj.StartDate)
                {
                    return new NewProjectionSummary(false, $"Projection overlaps with previous one: {previousProjectionMovie.Name} at {previousProjection.StartDate}");
                }
            }

            return newProj.New(proj);
        }
    }
}