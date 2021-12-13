#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using CourseProject.Domain.Entity;

#endregion

namespace CourseProject.Shared.IEntityService
{
    public interface IGenreService : IService<Genre>
    {
        public Task<IEnumerable<string>> GetExistedGenresAsync();

        public Task<IEnumerable<Genre>> NameFilt(string name);
    }
}