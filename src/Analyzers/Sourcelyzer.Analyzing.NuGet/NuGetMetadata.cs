using System;
using System.Diagnostics;
using NuGet.Configuration;
using NuGet.Packaging;
using NuGet.Versioning;

namespace Sourcelyzer.Analyzing.Nuget
{
    [DebuggerDisplay("{PackageName}-{Current}")]
    public class NuGetMetadata : IEquatable<NuGetMetadata>
    {
        internal NuGetMetadata(PackageReference current, (PackageSource PackageSource, NuGetVersion Version) latest)
        {
            PackageName = current?.PackageIdentity.Id ?? throw new ArgumentNullException(nameof(current));
            IsPrerelease = current.PackageIdentity.Version.IsPrerelease;
            Current = current.PackageIdentity.Version;

            Latest = latest.Version;
            PackageSource = latest.PackageSource;
        }

        public string PackageName { get; }

        public PackageSource PackageSource { get; }

        public bool IsPrerelease { get; }

        public bool IsOutdated => Latest != null && Latest > Current;

        public NuGetVersion Current { get; }

        public NuGetVersion Latest { get; }

        public bool Equals(NuGetMetadata other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(PackageName, other.PackageName) && Equals(PackageSource, other.PackageSource) &&
                   IsPrerelease == other.IsPrerelease && Equals(Current, other.Current) && Equals(Latest, other.Latest);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NuGetMetadata) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PackageName != null ? PackageName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (PackageSource != null ? PackageSource.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsPrerelease.GetHashCode();
                hashCode = (hashCode * 397) ^ (Current != null ? Current.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Latest != null ? Latest.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
