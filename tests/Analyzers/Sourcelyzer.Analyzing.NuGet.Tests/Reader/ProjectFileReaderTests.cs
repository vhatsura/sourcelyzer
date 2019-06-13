using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NuGet.Packaging;
using NuGet.Versioning;
using Sourcelyzer.Analyzing.NuGet.Reader;
using Xunit;

namespace Sourcelyzer.Analyzing.NuGet.Tests.Reader
{
    public class ProjectFileReaderTests
    {
        public static IEnumerable<object[]> GetPackagesTestData
        {
            get
            {
                yield return new object[]
                {
                    new XDocument()
                };

                yield return new object[]
                {
                    XDocument.Parse(@"
<Project Sdk=""Microsoft.NET.Sdk"">
    <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
    </PropertyGroup>
    <ItemGroup>
        <ProjectReference Include=""..\\Sourcelyzer\\Sourcelyzer.csproj"" />
    </ItemGroup>
    <ItemGroup>
        <PackageReference Include=""morelinq"" Version=""3.1.1"" />
        <PackageReference Include=""Octokit"" Version=""0.32.0"" />
    </ItemGroup>
</Project>"),
                    new Action<PackageReference>[]
                    {
                        package =>
                        {
                            Assert.Equal("morelinq", package.PackageIdentity.Id);
                            Assert.Equal(new NuGetVersion(3, 1, 1), package.PackageIdentity.Version);
                        },
                        package =>
                        {
                            Assert.Equal("Octokit", package.PackageIdentity.Id);
                            Assert.Equal(new NuGetVersion(0, 32, 0), package.PackageIdentity.Version);
                        }
                    }
                };

                yield return new object[]
                {
                    XDocument.Parse(@"
<Project Sdk=""Microsoft.NET.Sdk"">
    <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
    </PropertyGroup>
    <ItemGroup>
        <ProjectReference Include=""..\\Sourcelyzer\\Sourcelyzer.csproj"" />
    </ItemGroup>
    <ItemGroup>
        <PackageReference Include=""morelinq"" >
            <Version>3.1.1</Version>
        </PackageReference>
        <PackageReference Include=""Octokit"">
            <Version>0.32.0</Version>
        </PackageReference>
    </ItemGroup>
</Project>"),
                    new Action<PackageReference>[]
                    {
                        package =>
                        {
                            Assert.Equal("morelinq", package.PackageIdentity.Id);
                            Assert.Equal(new NuGetVersion(3, 1, 1), package.PackageIdentity.Version);
                        },
                        package =>
                        {
                            Assert.Equal("Octokit", package.PackageIdentity.Id);
                            Assert.Equal(new NuGetVersion(0, 32, 0), package.PackageIdentity.Version);
                        }
                    }
                };
                
                yield return new object[]
                {
                    XDocument.Parse(@"
<Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
    <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
    </PropertyGroup>
    <ItemGroup>
        <ProjectReference Include=""..\\Sourcelyzer\\Sourcelyzer.csproj"" />
    </ItemGroup>
    <ItemGroup>
        <PackageReference Include=""morelinq"" >
            <Version>3.1.1</Version>
        </PackageReference>
        <PackageReference Include=""Octokit"">
            <Version>0.32.0</Version>
        </PackageReference>
    </ItemGroup>
</Project>"),
                    new Action<PackageReference>[]
                    {
                        package =>
                        {
                            Assert.Equal("morelinq", package.PackageIdentity.Id);
                            Assert.Equal(new NuGetVersion(3, 1, 1), package.PackageIdentity.Version);
                        },
                        package =>
                        {
                            Assert.Equal("Octokit", package.PackageIdentity.Id);
                            Assert.Equal(new NuGetVersion(0, 32, 0), package.PackageIdentity.Version);
                        }
                    }
                };
            }
        }

        [Theory]
        [MemberData(nameof(GetPackagesTestData))]
        public void GetPackages_ShouldParseProjectFileCorrectly(XDocument document,
            params Action<PackageReference>[] elementInspectors)
        {
            // Arrange
            var reader = new ProjectFileReader();

            // Act
            var packages = reader.GetPackages(document);

            // Assert
            Assert.Collection(packages, elementInspectors);
        }
    }
}
