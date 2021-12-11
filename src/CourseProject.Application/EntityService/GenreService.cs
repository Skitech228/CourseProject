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
    public class GenreService : IGenreService
    {
        #region Implementation of IService<Artist>

        private readonly ApplicationContext _context;

        public GenreService(ApplicationContext context) => _context = context;

        public async Task<bool> AddAsync(Genre artist)
        {
            await _context.Genres.AddAsync(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(Genre artist)
        {
            _context.Genres.Remove(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Genre entity)
        {
            _context.Genres.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Genre> GetByIdAsync(int id) => await _context.Genres.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Genre>> GetAllAsync() => await _context.Genres.ToListAsync();

        #endregion
    }
}