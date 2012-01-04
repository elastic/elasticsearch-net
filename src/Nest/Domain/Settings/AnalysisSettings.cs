using System.Collections.Generic;

namespace Nest.Settings
{
    public class AnalysisSettings
    {
        public AnalysisSettings()
        {
            this.Analyzer = new Dictionary<string, AnalyzerSettings>();
            this.TokenFilters = new Dictionary<string, TokenFilterSettings>();
        }

        public IDictionary<string, AnalyzerSettings> Analyzer { get; private set; }
        public IDictionary<string, TokenFilterSettings> TokenFilters { get; set; }
    }
}