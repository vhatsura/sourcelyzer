using System;
using System.Collections.Generic;
using Octokit;

namespace Sourcelyzer.GitHub.Collecting.Filter
{
    public class Options
    {
        internal IList<Func<Repository, bool>> RepositoryFilters { get; } = new List<Func<Repository, bool>>();

        internal IList<string> OrganizationsToAnalyze { get; private set; } = new List<string>();

        public Options ForSpecifiedOrganizations(params string[] orgs)
        {
            if (orgs == null) throw new ArgumentNullException(nameof(orgs));

            OrganizationsToAnalyze = new List<string>(orgs);

            return this;
        }

        public Options WithFilter(Func<Repository, bool> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            RepositoryFilters.Add(filter);

            return this;
        }
    }
}
