using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sourcelyzer.Model
{
    public interface IRepository
    {
        long Id { get; }

        string Owner { get; }

        string Name { get; }

        Task<IEnumerable<IFile>> GetFilesAsync();
    }
}
