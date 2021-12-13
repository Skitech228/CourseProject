#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entity;

#endregion

namespace CourseProject.Shared.IEntityService
{
    public interface ISubscriptionService : IService<Subscription>
    {
        public Task<bool> AddAsync(Subscription artist, string service);

        public Task<bool> UpdateAsync(Subscription entity, string service);

        public Task<IEnumerable<Subscription>> CostFilt();
    }
}