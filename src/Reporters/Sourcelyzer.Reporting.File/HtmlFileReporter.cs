using System.IO;
using System.Threading.Tasks;
using Markdig;
using Sourcelyzer.Model.Analyzing;
using FileOptions = Sourcelyzer.Reporting.File.Options.FileOptions;

namespace Sourcelyzer.Reporting.File
{
    internal class HtmlFileReporter : BaseFileReporter
    {
        private readonly MarkdownPipeline _pipeline;

        public HtmlFileReporter(FileOptions options)
            : base(options)
        {
            _pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        }

        protected override string Extension => "html";


        protected override Task WriteIssueAsync(IAnalyzerResult result, TextWriter writer)
        {
            var html = Markdown.ToHtml(result.ToMarkdown(), _pipeline);

            return writer.WriteAsync(html);
        }
    }
}
