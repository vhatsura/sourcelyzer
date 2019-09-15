using System;
using System.Collections.Generic;
using System.Text;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Repository
{
    public class MissedReadmeFile : IAnalyzerResult
    {
        internal MissedReadmeFile(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IRepository Repository { get; }

        public string Title => "Missed README.md file";

        public string ShortTitle => Title;
        
        public IDictionary<string, string> TechnicalInfo => new Dictionary<string, string>();

        public string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("The repository must contain `README.md` file at the root level of repository.");

            return stringBuilder.ToString();
        }

        public Status ActualizeStatus(IDictionary<string, string> technicalInfo)
        {
            return Status.UpToDate;
        }
    }
}
