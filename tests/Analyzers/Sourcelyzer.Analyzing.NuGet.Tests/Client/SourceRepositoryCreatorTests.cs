using System;
using Sourcelyzer.Analyzing.NuGet.Client;
using Xunit;

namespace Sourcelyzer.Analyzing.NuGet.Tests.Client
{
    public class SourceRepositoryCreatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ShouldThrowException_WhenPackageSourceIsNullOrWhiteSpace(string packageSource)
        {
            // Arrange
            var creator = new SourceRepositoryCreator();

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => creator.Create(packageSource));
        }

        [Fact]
        public void Create_DoNotThrowException()
        {
            // Arrange
            var creator = new SourceRepositoryCreator();

            // Act + Assert
            creator.Create("source");
        }
    }
}
