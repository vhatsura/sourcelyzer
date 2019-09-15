using System;
using System.IO;
using System.Threading.Tasks;
using Sourcelyzer.Model.Analyzing;
using FileOptions = Sourcelyzer.Reporting.File.Options.FileOptions;

namespace Sourcelyzer.Reporting.File
{
    public abstract class BaseFileReporter : IReporter
    {
        private readonly FileOptions _options;

        protected BaseFileReporter(FileOptions options)
        {
            _options = options;

            if (!Directory.Exists(_options.Path))
                Directory.CreateDirectory(_options.Path);
        }

        protected abstract string Extension { get; }

        private string FormattedDate => DateTime.UtcNow.ToString("yyyy-MM-ddThh-mm-ss.fff");

        public async Task ReportAsync(IAnalyzerResult result)
        {
            var reportPath = Path.Combine(_options.Path,
                $"{result.Repository.Owner}.{result.Repository.Name}.{result.GetType().Name}.{Extension}");

            var fileStream = System.IO.File.Open(reportPath, FileMode.Append);
            using (var writer = new StreamWriter(fileStream))
            {
                await WriteIssueAsync(result, writer);
            }
        }

        protected abstract Task WriteIssueAsync(IAnalyzerResult result, TextWriter writer);
    }
}
