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
    public class SponsorService : ISponsorService
    {
        #region Implementation of IService<Artist>

        private readonly ApplicationContext _context;

        public SponsorService(ApplicationContext context) => _context = context;

        public async Task<bool> AddAsync(Sponsor artist)
        {
            await _context.Sponsors.AddAsync(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(Sponsor artist)
        {
            _context.Sponsors.Remove(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Sponsor entity)
        {
            _context.Sponsors.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Sponsor> GetByIdAsync(int id) => await _context.Sponsors.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Sponsor>> GetAllAsync() => await _context.Sponsors.ToListAsync();

        #endregion
    }
}