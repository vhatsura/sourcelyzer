using System;
using System.Text;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Config
{
    internal class MissedNuGetConfig : IAnalyzerResult
    {
        internal MissedNuGetConfig(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        public IRepository Repository { get; }

        public string Title => "Missed NuGet.config";

        public string ShortTitle => Title;
        
        public string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("The repository must contain `NuGet.config` file.");
            
            return stringBuilder.ToString();
        }
    }
}
