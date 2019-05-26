using System;
using Sourcelyzer.Analyzing.NuGet.Reader;
using Xunit;

namespace Sourcelyzer.Analyzing.NuGet.Tests.Reader
{
    public class PackageReferencesReaderFactoryTests
    {
        [Theory]
        [InlineData("packages.config", typeof(PackagesConfigReader))]
        [InlineData(".csproj", typeof(ProjectFileReader))]
        public void CreateReader_ShouldReturnAppropriateReader(string path, Type readerType)
        {
            // Arrange
            var factory = new PackageReferencesReaderFactory();
            
            // Act
            var reader = factory.CreateReader(path);

            // Assert
            Assert.Equal(readerType, reader.GetType());
        }
    }
}
