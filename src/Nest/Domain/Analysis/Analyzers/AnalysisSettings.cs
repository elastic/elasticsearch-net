using System.Collections.Generic;

namespace Nest
{
    public class AnalysisSettings
    {
        public AnalysisSettings()
        {
			this.Analyzers = new Dictionary<string, AnalyzerBase>();
            this.TokenFilters = new Dictionary<string, TokenFilterBase>();
			this.Tokenizers = new Dictionary<string, TokenizerBase>();
			this.CharFilters = new Dictionary<string, CharFilterBase>();
        }

		public IDictionary<string, AnalyzerBase> Analyzers { get; set; }
        public IDictionary<string, TokenFilterBase> TokenFilters { get; set; }
		public IDictionary<string, TokenizerBase> Tokenizers { get; set; }
		public IDictionary<string, CharFilterBase> CharFilters { get; set; }
    }
}