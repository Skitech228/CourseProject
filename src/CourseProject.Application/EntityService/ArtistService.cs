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
    public class ArtistService : IArtistService
    {
        #region Implementation of IService<Artist>

        private readonly ApplicationContext _context;

        public ArtistService(ApplicationContext context) => _context = context;

        public async Task<bool> AddAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(Artist artist)
        {
            _context.Artists.Remove(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Artist entity)
        {
            _context.Artists.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Artist> GetByIdAsync(int id) => await _context.Artists.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Artist>> GetAllAsync() => await _context.Artists.ToListAsync();

        #endregion
    }
}