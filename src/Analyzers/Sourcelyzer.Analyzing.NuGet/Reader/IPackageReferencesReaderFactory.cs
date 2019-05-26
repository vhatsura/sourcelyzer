namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public interface IPackageReferencesReaderFactory
    {
        IPackageReferencesReader CreateReader(string path);
    }
}
