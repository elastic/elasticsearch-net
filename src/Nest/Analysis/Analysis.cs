using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalysis
	{
		[JsonProperty("analyzer")]
		IAnalyzers Analyzers { get; set; }
		[JsonProperty("char_filter")]
		ICharFilters CharFilters { get; set; }
		[JsonProperty("filter")]
		ITokenFilters TokenFilters { get; set; }
		[JsonProperty("tokenizer")]
		ITokenizers Tokenizers { get; set; }
	}

	public class Analysis : IAnalysis
	{
		public IAnalyzers Analyzers { get; set; }
		public ICharFilters CharFilters { get; set; }
		public ITokenFilters TokenFilters { get; set; }
		public ITokenizers Tokenizers { get; set; }

	}

	public class AnalysisDescriptor : DescriptorBase<AnalysisDescriptor, IAnalysis>, IAnalysis
	{
		IAnalyzers IAnalysis.Analyzers { get; set; }
		ICharFilters IAnalysis.CharFilters { get; set; }
		ITokenFilters IAnalysis.TokenFilters { get; set; }
		ITokenizers IAnalysis.Tokenizers { get; set; }

		public AnalysisDescriptor Analyzers(Func<AnalyzersDescriptor, IAnalyzers> selector) =>
			Assign(a => a.Analyzers = selector?.Invoke(new AnalyzersDescriptor()));

		public AnalysisDescriptor CharFilters(Func<CharFiltersDescriptor, ICharFilters> selector) =>
			Assign(a => a.CharFilters = selector?.Invoke(new CharFiltersDescriptor()));

		public AnalysisDescriptor TokenFilters(Func<TokenFiltersDescriptor, ITokenFilters> selector) =>
			Assign(a => a.TokenFilters = selector?.Invoke(new TokenFiltersDescriptor()));

		public AnalysisDescriptor Tokenizers(Func<TokenizersDescriptor, ITokenizers> selector) =>
			Assign(a => a.Tokenizers = selector?.Invoke(new TokenizersDescriptor()));

	}
}