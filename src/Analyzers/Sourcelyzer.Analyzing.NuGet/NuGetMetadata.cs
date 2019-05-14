using System;
using NuGet.Configuration;
using NuGet.Packaging;

namespace Sourcelyzer.Analyzing.Nuget
{
    public class NuGetMetadata
    {
        internal NuGetMetadata(PackageReference current, (PackageSource PackageSource, Version Version) latest)
        {
            PackageName = current?.PackageIdentity.Id ?? throw new ArgumentNullException(nameof(current));
            IsPrerelease = current.PackageIdentity.Version.IsPrerelease;
            Current = current.PackageIdentity.Version.Version;

            Latest = latest.Version;
            PackageSource = latest.PackageSource;
        }

        public string PackageName { get; }

        public PackageSource PackageSource { get; }

        public bool IsPrerelease { get; }

        public bool IsOutdated => Latest != null && Latest > Current;

        public Version Current { get; }

        public Version Latest { get; }
    }
}
