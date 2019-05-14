using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;
using Sourcelyzer.Analyzing.NuGet.Client;
using Sourcelyzer.Analyzing.Nuget.Outdated;
using Sourcelyzer.Analyzing.NuGet.Reader;
using Sourcelyzer.Model;
using Xunit;

namespace Sourcelyzer.Analyzing.NuGet.Tests
{
    public class OutdatedNugetAnalyzerTests
    {
        [Theory]
        [InlineData("file.xml", true)]
        [InlineData("packages.config", false)]
        [InlineData("project.csproj", false)]
        public async Task AnalyzeAsync_ShouldAnalyzeOnlyApplicableFiles(string path, bool isFiltered)
        {
            // Arrange
            var readerMock = new Mock<INuGetReferencesReader>();
            var clientMock = new Mock<INuGetClient>();
            var repoMock = new Mock<IRepository>();
            var fileMock = new Mock<IFile>();

            fileMock.SetupGet(x => x.Path)
                .Returns(() => path);

            repoMock.Setup(x => x.GetFilesAsync())
                .ReturnsAsync(() => new List<IFile> {fileMock.Object});

            var analyzer = new OutdatedNugetAnalyzer(readerMock.Object, clientMock.Object);

            // Act
            var results = await analyzer.AnalyzeAsync(repoMock.Object);

            // Assert
            readerMock.Verify(
                x => x.GetPackagesAsync(It.Is<IFile>(f => f == fileMock.Object)),
                isFiltered
                    ? Times.Never()
                    : Times.Once());
        }

        [Fact]
        public async Task AnalyzeAsync_GetMaxVersionsFromFirstPackageSource()
        {
            // Arrange
            var readerMock = new Mock<INuGetReferencesReader>();
            var clientMock = new Mock<INuGetClient>();
            var repoMock = new Mock<IRepository>();
            var fileMock = new Mock<IFile>();

            fileMock.SetupGet(x => x.Path)
                .Returns(() => "packages.config");

            repoMock.Setup(x => x.GetFilesAsync())
                .ReturnsAsync(() => new List<IFile> {fileMock.Object});

            readerMock.Setup(x => x.GetPackagesAsync(It.IsAny<IFile>()))
                .ReturnsAsync(() => new List<PackageReference>
                {
                    new PackageReference(new PackageIdentity("SomeLibrary", new NuGetVersion("1.0.0")),
                        new NuGetFramework("netstandard2.0"))
                });

            clientMock.Setup(x => x.GetAllVersions(It.IsAny<PackageReference>()))
                .Returns(() => new List<(PackageSource, IList<NuGetVersion>)>
                {
                    (new PackageSource("1"),
                        new List<NuGetVersion>
                        {
                            new NuGetVersion("1.0.0"),
                            new NuGetVersion("1.1.0"),
                            new NuGetVersion("1.1.1")
                        }),
                    (new PackageSource("2"), new List<NuGetVersion>
                    {
                        new NuGetVersion("1.0.0"),
                        new NuGetVersion("1.1.0"),
                        new NuGetVersion("1.1.1"),
                        new NuGetVersion("1.1.2")
                    })
                });

            var analyzer = new OutdatedNugetAnalyzer(readerMock.Object, clientMock.Object);

            // Act
            var result = await analyzer.AnalyzeAsync(repoMock.Object);

            // Assert
            Assert.IsType<OutdatedNuGetResult>(result);

            var outdatedNuGets = (OutdatedNuGetResult) result;
            Assert.Single(outdatedNuGets.OutdatedNuGets);

            Assert.Collection(outdatedNuGets.OutdatedNuGets,
                x => Assert.Collection(x.Value, n =>
                {
                    Assert.Equal("SomeLibrary", n.PackageName);
                    Assert.Equal("2", n.PackageSource.Name);
                    Assert.Equal(new Version("1.1.2.0"), n.Latest);
                }));
        }
    }
}
