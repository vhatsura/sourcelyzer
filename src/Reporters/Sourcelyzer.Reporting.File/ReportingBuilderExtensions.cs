using System;
using Sourcelyzer.Reporting.File.Options;

namespace Sourcelyzer.Reporting.File
{
    public static class ReportingBuilderExtensions
    {
        public static SourcelyzerBuilder AsHtmlFile(this ReportingBuilder reportingBuilder, string path,
            Action<FileOptions> configure = null)
        {
            var options = new FileOptions(path);
            configure?.Invoke(options);

            reportingBuilder.Configuration.AddReporter(new HtmlFileReporter(options));

            return reportingBuilder.Builder;
        }
    }
}
