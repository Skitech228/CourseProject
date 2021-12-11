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
    public class SubscriptionService : ISubscriptionService
    {
        #region Implementation of IService<Artist>

        private readonly ApplicationContext _context;

        public SubscriptionService(ApplicationContext context) => _context = context;

        public async Task<bool> AddAsync(Subscription artist)
        {
            await _context.Subscriptions.AddAsync(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveAsync(Subscription artist)
        {
            _context.Subscriptions.Remove(artist);

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Subscription entity)
        {
            _context.Subscriptions.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<Subscription> GetByIdAsync(int id) => await _context.Subscriptions.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Subscription>> GetAllAsync() => await _context.Subscriptions.ToListAsync();

        #endregion
    }
}