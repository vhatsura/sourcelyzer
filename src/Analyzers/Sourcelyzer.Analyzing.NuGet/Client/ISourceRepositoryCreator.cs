using NuGet.Protocol.Core.Types;

namespace Sourcelyzer.Analyzing.NuGet.Client
{
    internal interface ISourceRepositoryCreator
    {
        SourceRepository Create(string packageSource);
    }
}
