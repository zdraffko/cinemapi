using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<IProjection> GetById(long projectionId)
        {
            return await db.Projections.FirstOrDefaultAsync(p => p.Id == projectionId);
        }

        public async Task<IProjection> Get(int movieId, int roomId, DateTime startDate)
        {
            return await db.Projections.FirstOrDefaultAsync(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate);
        }

        public IEnumerable<IProjection> GetActiveProjections(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now);
        }

        public async Task Insert(IProjectionCreation proj)
        {
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount, proj.IsReservable);

            db.Projections.Add(newProj);
            await db.SaveChangesAsync();
        }

        public async Task DecreaseAvailableSeats(long projectionId, int seatsCount)
        {
            Projection projection = await this.GetById(projectionId) as Projection;

            if (projection == null)
            {
                return;
            }

            projection.AvailableSeatsCount -= seatsCount;

            await db.SaveChangesAsync();
        }

        public async Task IncreaseAvailableSeats(long projectionId, int seatsCount)
        {
            Projection projection = await this.GetById(projectionId) as Projection;

            if (projection == null)
            {
                return;
            }

            projection.AvailableSeatsCount += seatsCount;

            await db.SaveChangesAsync();
        }

        public async Task ChangeReservationPolicy(long projectionId, bool isReservable)
        {
            Projection projection = await this.GetById(projectionId) as Projection;

            if (projection == null)
            {
                return;
            }

            projection.IsReservable = isReservable;

            await db.SaveChangesAsync();
        }
    }
}