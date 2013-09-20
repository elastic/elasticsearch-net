using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

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

		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<string, AnalyzerBase> Analyzers { get; set; }

		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<string, TokenFilterBase> TokenFilters { get; set; }
	
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<string, TokenizerBase> Tokenizers { get; set; }
		
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<string, CharFilterBase> CharFilters { get; set; }
    }
}