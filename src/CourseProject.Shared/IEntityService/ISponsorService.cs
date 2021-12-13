#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entity;

#endregion

namespace CourseProject.Shared.IEntityService
{
    public interface ISponsorService : IService<Sponsor>
    {
        public Task<IEnumerable<Sponsor>> NameFilt();
    }
}