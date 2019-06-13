using System;
using System.Collections.Generic;
using System.Text;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Config
{
    internal class MissedNuGetConfig : NuGetAnalyzerResult
    {
        internal MissedNuGetConfig(IRepository repository)
            : base(repository)
        {
        }

        public override string Title => "Missed NuGet.config";

        public override string ShortTitle => Title;

        public override IDictionary<string, string> TechnicalInfo => new Dictionary<string, string>();

        public override string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("The repository must contain `NuGet.config` file.");

            return stringBuilder.ToString();
        }
    }
}
