using System;
using System.IO;
using System.Threading.Tasks;
using Sourcelyzer.Model.Analyzing;
using Sourcelyzer.Reporting.File.Options;
using FileOptions = Sourcelyzer.Reporting.File.Options.FileOptions;

namespace Sourcelyzer.Reporting.File
{
    public abstract class BaseFileReporter : IReporter
    {
        private readonly FileOptions _options;

        protected BaseFileReporter(FileOptions options)
        {
            _options = options;
        }

        protected abstract string Extension { get; }

        private string FormattedDate => DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss.fff");

        public async Task ReportAsync(IAnalyzerResult result)
        {
            switch (_options.Segregation)
            {
                case SegregationType.AllInOne:
                {
//                    var filePath = Path.Combine(_options.Path,
//                        $"report.{DateTime.UtcNow:yyyy-MM-ddThh:mm:ss.fff}.{Extension}");
//                    using (var fileStream = System.IO.File.Create(filePath))
//                    {
//                        
//                    }
                }
                    throw new NotImplementedException();
                case SegregationType.DirectoryPerRepository:
                {
                    var directoryPath = Path.Combine(_options.Path, result.Repository.Owner, result.Repository.Name);
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    var reportPath = Path.Combine(
                        directoryPath,
                        $"report.{result.GetType().Name}.{FormattedDate}.{Extension}");
                    
                    using (var fileStream = System.IO.File.Create(reportPath))
                    using (var writer = new StreamWriter(fileStream))
                    {
                        await WriteIssueAsync(result, writer);
                    }

                    break;    
                }  
                case SegregationType.DirectoryPerIssue:
                    throw new NotImplementedException();
                case SegregationType.FilePerRepository:
                    throw new NotImplementedException();
                case SegregationType.FilePerIssue:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract Task WriteIssueAsync(IAnalyzerResult result, TextWriter writer);
    }
}
