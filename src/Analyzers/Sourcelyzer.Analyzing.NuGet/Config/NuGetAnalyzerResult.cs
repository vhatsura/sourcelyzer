using System;
using System.Collections.Generic;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Config
{
    public abstract class NuGetAnalyzerResult : IAnalyzerResult
    {
        protected NuGetAnalyzerResult(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IRepository Repository { get; }

        public abstract string Title { get; }

        public abstract string ShortTitle { get; }
        
        public abstract IDictionary<string, string> TechnicalInfo { get; }

        public abstract string ToMarkdown();
        public Status ActualizeStatus(IDictionary<string, string> technicalInfo)
        {
            return Status.UpToDate;
        }
    }
}
