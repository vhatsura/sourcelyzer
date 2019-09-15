using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moq;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Sourcelyzer.Analyzing.NuGet.Client;
using Xunit;

namespace Sourcelyzer.Analyzing.NuGet.Tests.Client
{
    public class NuGetClientTests
    {
        private Mock<SourceRepository> GetSourceRepositoryMock<TResource>(TResource resource, string packageSource)
            where TResource : class, INuGetResource
        {
            var mock = new Mock<SourceRepository>();

            mock.Setup(x => x.GetResourceAsync<TResource>(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => resource);

            mock.SetupGet(x => x.PackageSource)
                .Returns(() => new PackageSource(packageSource));

            return mock;
        }

        private void SetupSourceRepository(Mock<ISourceRepositoryCreator> mock, string packageSource,
            SourceRepository repository)
        {
            mock.Setup(x => x.Create(It.Is<string>(s => s.Equals(packageSource))))
                .Returns(() => repository);
        }

        private Mock<TResource> GetResourceMock<TResource>(IEnumerable<NuGetVersion> versions)
            where TResource : FindPackageByIdResource
        {
            var mock = new Mock<TResource>();

            mock.Setup(x => x.GetAllVersionsAsync(It.IsAny<string>(), It.IsAny<SourceCacheContext>(),
                    It.IsAny<ILogger>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => versions);

            return mock;
        }

        private static PackageReference GetPackageReference()
        {
            return new PackageReference(
                new PackageIdentity(
                    "SomeLibrary",
                    new NuGetVersion("2.2.5148")),
                new NuGetFramework("net46"));
        }

        [Fact]
        public void GetAllVersions_ShouldReturnOriginalVersion_WhenResourceIsNull()
        {
            // Arrange
            const string nuGetSource = "https://api.nuget.org/v3/index.json";

            var nuGetSourceMock = GetSourceRepositoryMock<FindPackageByIdResource>(null, nuGetSource);

            var sourceCreatorMock = new Mock<ISourceRepositoryCreator>();
            SetupSourceRepository(sourceCreatorMock, nuGetSource, nuGetSourceMock.Object);

            var nugetClient = new NuGetClient(new List<string> {nuGetSource}, sourceCreatorMock.Object);

            // Act
            var versions = nugetClient
                .GetAllVersions(GetPackageReference())
                .ToList();

            // Assert
            Assert.Single(versions);
            Assert.Collection(versions, x =>
            {
                Assert.Single(x.Versions);
                Assert.Equal(new NuGetVersion("2.2.5148"), x.Versions.First());
            });
        }

        [Fact]
        public void GetAllVersions_ShouldTryAllSourceRepositories()
        {
            // Arrange
            const string nuGetSource = "https://api.nuget.org/v3/index.json";
            const string myGetSource = "https://www.myget.org/F/sourcelyzer";

            var nugetVersions = new List<NuGetVersion>
            {
                new NuGetVersion("3.0.0"),
                new NuGetVersion("3.1.0-alpha0001")
            };

            var nugetResourceMock = GetResourceMock<FindPackageByIdResource>(nugetVersions);
            var nugetSourceMock = GetSourceRepositoryMock(nugetResourceMock.Object, nuGetSource);

            var myGetVersions = new List<NuGetVersion>
            {
                new NuGetVersion("3.0.0"),
                new NuGetVersion("3.1.0-alpha0001"),
                new NuGetVersion("3.1.0-feature0002"),
                new NuGetVersion("3.1.0-feature0003")
            };

            var myGetResourceMock = GetResourceMock<FindPackageByIdResource>(myGetVersions);
            var myGetSourceMock = GetSourceRepositoryMock(myGetResourceMock.Object, myGetSource);


            var sourceCreatorMock = new Mock<ISourceRepositoryCreator>();
            SetupSourceRepository(sourceCreatorMock, nuGetSource, nugetSourceMock.Object);
            SetupSourceRepository(sourceCreatorMock, myGetSource, myGetSourceMock.Object);

            var nugetClient = new NuGetClient(new List<string>
                {nuGetSource, myGetSource}, sourceCreatorMock.Object);

            // Act
            var versions = nugetClient
                .GetAllVersions(GetPackageReference())
                .ToList();

            // Assert
            Assert.Equal(2, versions.Count);
            Assert.Equal(nugetVersions, versions.First(x => x.PackageSource.Name == nuGetSource).Versions);
            Assert.Equal(myGetVersions, versions.First(x => x.PackageSource.Name == myGetSource).Versions);
        }
    }
}
