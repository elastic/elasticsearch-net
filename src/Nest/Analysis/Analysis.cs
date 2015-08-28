using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalysis
	{
		IAnalyzers Analyzers { get; set; }
		ICharFilters CharFilters { get; set; }
		ITokenFilters TokenFilters { get; set; }
		ITokenizers Tokenizers { get; set; }
	}

	public class AnalysisSettings : IAnalysis
	{
		public IAnalyzers Analyzers { get; set; }
		public ICharFilters CharFilters { get; set; }
		public ITokenFilters TokenFilters { get; set; }
		public ITokenizers Tokenizers { get; set; }

	}

	public class AnalysisSettingsDescriptor : IAnalysis
	{
		protected AnalysisSettingsDescriptor Assign(Action<IAnalysis> assigner) => Fluent.Assign(this, assigner);

		IAnalyzers IAnalysis.Analyzers { get; set; }
		ICharFilters IAnalysis.CharFilters { get; set; }
		ITokenFilters IAnalysis.TokenFilters { get; set; }
		ITokenizers IAnalysis.Tokenizers { get; set; }

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