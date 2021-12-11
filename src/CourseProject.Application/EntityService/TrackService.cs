#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Database;
using CourseProject.Domain.Entity;
using CourseProject.Shared.IEntityService;
using Microsoft.EntityFrameworkCore;

#endregion

namespace CourseProject.Application.EntityService
{
    public class TrackService : ITrackService
    {
        #region Implementation of IService<Artist>

        private readonly ApplicationContext _context;

        public TrackService(ApplicationContext context) => _context = context;

        public async Task<bool> AddAsync(Track artist)
        {
            await _context.Tracks.AddAsync(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(Track artist)
        {
            _context.Tracks.Remove(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Track entity)
        {
            _context.Tracks.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Track> GetByIdAsync(int id) => await _context.Tracks.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Track>> GetAllAsync() => await _context.Tracks.ToListAsync();

        #endregion
    }
}