using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Sourcelyzer.Analyzing.NuGet.Client;
using Sourcelyzer.Analyzing.Nuget.Outdated;
using Sourcelyzer.Analyzing.NuGet.Reader;
using Sourcelyzer.Model;
using Xunit;

namespace Sourcelyzer.Analyzing.NuGet.Tests
{
    public class OutdatedNugetAnalyzerTests
    {
        private readonly OutdatedNugetAnalyzer _analyzer;
        private readonly Mock<INuGetClient> _clientMock;
        private readonly Mock<IFile> _fileMock;
        private readonly Mock<INuGetReferencesReader> _readerMock;
        private readonly Mock<IRepository> _repositoryMock;

        public OutdatedNugetAnalyzerTests()
        {
            _readerMock = new Mock<INuGetReferencesReader>();
            _clientMock = new Mock<INuGetClient>();
            _fileMock = new Mock<IFile>();
            _repositoryMock = new Mock<IRepository>();

            _repositoryMock.Setup(x => x.GetFilesAsync())
                .ReturnsAsync(() => new List<IFile> {_fileMock.Object});

            _analyzer = new OutdatedNugetAnalyzer(_readerMock.Object, _clientMock.Object);
        }

        [Theory]
        [InlineData("file.xml", true)]
        [InlineData("packages.config", false)]
        [InlineData("project.csproj", false)]
        public async Task AnalyzeAsync_ShouldAnalyzeOnlyApplicableFiles(string path, bool isFiltered)
        {
            // Arrange
            _fileMock.SetupGet(x => x.Path).Returns(() => path);

            // Act
            var results = await _analyzer.AnalyzeAsync(_repositoryMock.Object);

            // Assert
            _readerMock.Verify(
                x => x.GetPackagesAsync(It.Is<IFile>(f => f == _fileMock.Object)),
                isFiltered
                    ? Times.Never()
                    : Times.Once());
        }

//        [Fact]
//        public async Task AnalyzeAsync_GetMaxVersionsFromFirstPackageSource()
//        {
//            // Arrange
//            _fileMock.SetupGet(x => x.Path).Returns(() => "packages.config");
//
//            _readerMock.Setup(x => x.GetPackagesAsync(It.IsAny<IFile>()))
//                .ReturnsAsync(() => new List<PackageReference>
//                {
//                    new PackageReference(new PackageIdentity("SomeLibrary", new NuGetVersion("1.0.0")),
//                        new NuGetFramework("netstandard2.0"))
//                });
//
//            _clientMock.Setup(x => x.GetAllVersions(It.IsAny<PackageReference>()))
//                .Returns(() => new List<(PackageSource, IList<NuGetVersion>)>
//                {
//                    (new PackageSource("1"),
//                        new List<NuGetVersion>
//                        {
//                            new NuGetVersion("1.0.0"),
//                            new NuGetVersion("1.1.0"),
//                            new NuGetVersion("1.1.1")
//                        }),
//                    (new PackageSource("2"), new List<NuGetVersion>
//                    {
//                        new NuGetVersion("1.0.0"),
//                        new NuGetVersion("1.1.0"),
//                        new NuGetVersion("1.1.1"),
//                        new NuGetVersion("1.1.2")
//                    })
//                });
//
//            // Act
//            var result = await _analyzer.AnalyzeAsync(_repositoryMock.Object);
//
//            // Assert
//            Assert.IsType<OutdatedNuGetResult>(result);
//
//            var outdatedNuGets = (OutdatedNuGetResult) result;
//            Assert.Single(outdatedNuGets.OutdatedNuGets);
//
//            Assert.Collection(outdatedNuGets.OutdatedNuGets,
//                x => Assert.Collection(x.Value, n =>
//                {
//                    Assert.Equal("SomeLibrary", n.PackageName);
//                    Assert.Equal("2", n.PackageSource.Name);
//                    Assert.Equal(new Version("1.1.2.0"), n.Latest);
//                }));
//        }
    }
}
