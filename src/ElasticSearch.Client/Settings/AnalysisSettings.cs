using System.Collections.Generic;

namespace ElasticSearch.Client.Settings
{
    public class AnalysisSettings
    {
        public AnalysisSettings()
        {
            this.Analyzer = new Dictionary<string, AnalyzerSettings>();
        }

        public IDictionary<string, AnalyzerSettings> Analyzer { get; private set; }
    }
}