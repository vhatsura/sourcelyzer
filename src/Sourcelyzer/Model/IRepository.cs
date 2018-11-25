using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sourcelyzer.Model
{
    public interface IRepository
    {
        Task<IEnumerable<IFile>> GetFilesAsync();
    }
}