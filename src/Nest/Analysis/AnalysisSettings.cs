using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalysisSettings
	{
		IAnalyzers Analyzers { get; set; }
	}

	public class AnalysisSettings : IAnalysisSettings
	{
		public AnalysisSettings()
		{
			this.TokenFilters = new Dictionary<string, TokenFilterBase>();
			this.Tokenizers = new Dictionary<string, TokenizerBase>();
			this.CharFilters = new Dictionary<string, CharFilterBase>();
		}

		//TODO all these dictionaries should be ProxyDictionary subclasses
		//[JsonConverter(typeof(AnalyzerCollectionJsonConverter))]
		public IAnalyzers Analyzers { get; set; }

		[JsonConverter(typeof(TokenFilterCollectionJsonConverter))]
		public IDictionary<string, TokenFilterBase> TokenFilters { get; set; }

		[JsonConverter(typeof(TokenizerCollectionJsonConverter))]
		public IDictionary<string, TokenizerBase> Tokenizers { get; set; }

		[JsonConverter(typeof(CharFilterCollectionJsonConverter))]
		public IDictionary<string, CharFilterBase> CharFilters { get; set; }
	}

	public class AnalysisSettingsDescriptor : IAnalysisSettings
	{
		protected AnalysisSettingsDescriptor Assign(Action<IAnalysisSettings> assigner) => Fluent.Assign(this, assigner);

		IAnalyzers IAnalysisSettings.Analyzers { get; set; }

		public AnalysisSettingsDescriptor Analyzers(Func<AnalyzersDescriptor, IAnalyzers> selector) =>
			Assign(a => a.Analyzers = selector?.Invoke(new AnalyzersDescriptor()));
	}
}