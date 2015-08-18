using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(AnalysisSettingsJsonConverter))]
	public class AnalysisSettings
	{
		public AnalysisSettings()
		{
			this.Analyzers = new Dictionary<string, AnalyzerBase>();
			this.TokenFilters = new Dictionary<string, TokenFilterBase>();
			this.Tokenizers = new Dictionary<string, TokenizerBase>();
			this.CharFilters = new Dictionary<string, CharFilterBase>();
		}

		//TODO all these dictionaries should be ProxyDictionary subclasses

		[JsonConverter(typeof(AnalyzerCollectionJsonConverter))]
		public IDictionary<string, AnalyzerBase> Analyzers { get; set; }

		[JsonConverter(typeof(TokenFilterCollectionJsonConverter))]
		public IDictionary<string, TokenFilterBase> TokenFilters { get; set; }

		[JsonConverter(typeof(TokenizerCollectionJsonConverter))]
		public IDictionary<string, TokenizerBase> Tokenizers { get; set; }

		[JsonConverter(typeof(CharFilterCollectionJsonConverter))]
		public IDictionary<string, CharFilterBase> CharFilters { get; set; }
	}
}