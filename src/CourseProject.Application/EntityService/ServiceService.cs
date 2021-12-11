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
    public class ServiceService : IServiceService
    {
        #region Implementation of IService<Artist>

        private readonly ApplicationContext _context;

        public ServiceService(ApplicationContext context) => _context = context;

        public async Task<bool> AddAsync(Service artist)
        {
            await _context.Services.AddAsync(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(Service artist)
        {
            _context.Services.Remove(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Service entity)
        {
            _context.Services.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Service> GetByIdAsync(int id) => await _context.Services.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Service>> GetAllAsync() => await _context.Services.ToListAsync();

        #endregion
    }
}