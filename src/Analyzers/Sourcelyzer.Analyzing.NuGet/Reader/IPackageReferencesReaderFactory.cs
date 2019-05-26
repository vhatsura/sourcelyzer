using System.Xml.Linq;
using NuGet.Packaging;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public interface IPackageReferencesReaderFactory
    {
        IPackageReferencesReader CreateReader(string path);
    }
}
