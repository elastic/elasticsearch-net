using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalysisSettings
	{
		IAnalyzers Analyzers { get; set; }
		ICharFilters CharFilters { get; set; }
		ITokenFilters TokenFilters { get; set; }
	}

	public class AnalysisSettings : IAnalysisSettings
	{
		public AnalysisSettings()
		{
			this.Tokenizers = new Dictionary<string, TokenizerBase>();
		}

		//TODO all these dictionaries should be ProxyDictionary subclasses
		//[JsonConverter(typeof(AnalyzerCollectionJsonConverter))]
		public IAnalyzers Analyzers { get; set; }
		public ICharFilters CharFilters { get; set; }
		public ITokenFilters TokenFilters { get; set; }

		[JsonConverter(typeof(TokenizerCollectionJsonConverter))]
		public IDictionary<string, TokenizerBase> Tokenizers { get; set; }

	}

	public class AnalysisSettingsDescriptor : IAnalysisSettings
	{
		protected AnalysisSettingsDescriptor Assign(Action<IAnalysisSettings> assigner) => Fluent.Assign(this, assigner);

		IAnalyzers IAnalysisSettings.Analyzers { get; set; }
		ICharFilters IAnalysisSettings.CharFilters { get; set; }
		ITokenFilters IAnalysisSettings.TokenFilters { get; set; }

		public AnalysisSettingsDescriptor Analyzers(Func<AnalyzersDescriptor, IAnalyzers> selector) =>
			Assign(a => a.Analyzers = selector?.Invoke(new AnalyzersDescriptor()));

		public AnalysisSettingsDescriptor CharFilters(Func<CharFiltersDescriptor, ICharFilters> selector) =>
			Assign(a => a.CharFilters = selector?.Invoke(new CharFiltersDescriptor()));

		public AnalysisSettingsDescriptor TokenFilters(Func<TokenFiltersDescriptor, ITokenFilters> selector) =>
			Assign(a => a.TokenFilters = selector?.Invoke(new TokenFiltersDescriptor()));

	}
}