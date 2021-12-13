#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entity;

#endregion

namespace CourseProject.Shared.IEntityService
{
    public interface ITrackService : IService<Track>
    {
        public Task<bool> UpdateAsync(Track entity, string artist, string genre);

        public Task<bool> AddAsync(Track entity, string artist, string genre);

        public Task<IEnumerable<Track>> NameFilt(string name);
    }
}