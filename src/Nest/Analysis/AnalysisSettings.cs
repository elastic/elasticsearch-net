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
		ITokenizers Tokenizers { get; set; }
	}

	public class AnalysisSettings : IAnalysisSettings
	{
		public IAnalyzers Analyzers { get; set; }
		public ICharFilters CharFilters { get; set; }
		public ITokenFilters TokenFilters { get; set; }
		public ITokenizers Tokenizers { get; set; }

	}

	public class AnalysisSettingsDescriptor : IAnalysisSettings
	{
		protected AnalysisSettingsDescriptor Assign(Action<IAnalysisSettings> assigner) => Fluent.Assign(this, assigner);

		IAnalyzers IAnalysisSettings.Analyzers { get; set; }
		ICharFilters IAnalysisSettings.CharFilters { get; set; }
		ITokenFilters IAnalysisSettings.TokenFilters { get; set; }
		ITokenizers IAnalysisSettings.Tokenizers { get; set; }

		public AnalysisSettingsDescriptor Analyzers(Func<AnalyzersDescriptor, IAnalyzers> selector) =>
			Assign(a => a.Analyzers = selector?.Invoke(new AnalyzersDescriptor()));

		public AnalysisSettingsDescriptor CharFilters(Func<CharFiltersDescriptor, ICharFilters> selector) =>
			Assign(a => a.CharFilters = selector?.Invoke(new CharFiltersDescriptor()));

		public AnalysisSettingsDescriptor TokenFilters(Func<TokenFiltersDescriptor, ITokenFilters> selector) =>
			Assign(a => a.TokenFilters = selector?.Invoke(new TokenFiltersDescriptor()));

		public AnalysisSettingsDescriptor Tokenizers(Func<TokenizersDescriptor, ITokenizers> selector) =>
			Assign(a => a.Tokenizers = selector?.Invoke(new TokenizersDescriptor()));

	}
}