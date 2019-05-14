using System.Threading.Tasks;
using System.Xml.Linq;
using Moq;
using NuGet.Packaging;
using Sourcelyzer.Analyzing.NuGet.Reader;
using Sourcelyzer.Model;
using Xunit;

namespace Sourcelyzer.Analyzing.NuGet.Tests.Reader
{
    public class NuGetReferencesReaderTests
    {
        [Fact]
        public async Task GetPackagesAsync_ShouldCallPackagesConfigReader_WhenFileIsPackagesConfig()
        {
            // Arrange
            var factoryMock = new Mock<IPackagesConfigReaderFactory>();
            var reader = new NuGetReferencesReader(factoryMock.Object);

            var fileMock = new Mock<IFile>();

            fileMock.Setup(x => x.Path)
                .Returns(() => "packages.config");

            fileMock.Setup(x => x.GetContentAsync())
                .ReturnsAsync(() => "<html></html>");

            // Act
            await reader.GetPackagesAsync(fileMock.Object);

            // Assert
            factoryMock.Verify(x => x.CreateReader(It.IsAny<XDocument>()), Times.Once);
        }
    }
}
