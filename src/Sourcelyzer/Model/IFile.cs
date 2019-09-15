using System.Threading.Tasks;

namespace Sourcelyzer.Model
{
    public interface IFile
    {
        string Path { get; }

        Task<string> GetContentAsync();
    }
}
