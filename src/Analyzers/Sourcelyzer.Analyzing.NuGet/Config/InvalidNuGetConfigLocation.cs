using System.Text;
using Sourcelyzer.Model;

namespace Sourcelyzer.Analyzing.Nuget.Config
{
    public class InvalidNuGetConfigLocation : NuGetAnalyzerResult
    {
        internal InvalidNuGetConfigLocation(IRepository repository, string nugetConfigPath, string solutionFilePath) :
            base(repository)
        {
            NuGetConfigPath = nugetConfigPath;
            SolutionFilePath = solutionFilePath;
        }

        public override string Title => "Invalid location of NuGet.config";

        public override string ShortTitle => Title;
        
        private string NuGetConfigPath { get; }
        private string SolutionFilePath { get; }

        public override string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(
                "The repository has `NuGet.config` in the wrong location. It must be located beside solution file.");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Expected: `{SolutionFilePath}`");
            stringBuilder.AppendLine($"Actual: `{NuGetConfigPath}`");

            return stringBuilder.ToString();
        }
    }
}
