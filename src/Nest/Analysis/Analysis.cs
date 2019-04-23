using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAnalysis
	{
		[JsonProperty("analyzer")]
		IAnalyzers Analyzers { get; set; }

		[JsonProperty("char_filter")]
		ICharFilters CharFilters { get; set; }

		[JsonProperty("normalizer")]
		INormalizers Normalizers { get; set; }

		[JsonProperty("filter")]
		ITokenFilters TokenFilters { get; set; }

		[JsonProperty("tokenizer")]
		ITokenizers Tokenizers { get; set; }
	}

	public class Analysis : IAnalysis
	{
		public IAnalyzers Analyzers { get; set; }
		public ICharFilters CharFilters { get; set; }

		public INormalizers Normalizers { get; set; }
		public ITokenFilters TokenFilters { get; set; }
		public ITokenizers Tokenizers { get; set; }
	}

	public class AnalysisDescriptor : DescriptorBase<AnalysisDescriptor, IAnalysis>, IAnalysis
	{
		IAnalyzers IAnalysis.Analyzers { get; set; }
		ICharFilters IAnalysis.CharFilters { get; set; }
		INormalizers IAnalysis.Normalizers { get; set; }
		ITokenFilters IAnalysis.TokenFilters { get; set; }
		ITokenizers IAnalysis.Tokenizers { get; set; }

		public AnalysisDescriptor Analyzers(Func<AnalyzersDescriptor, IPromise<IAnalyzers>> selector) =>
			Assign(selector, (a, v) => a.Analyzers = v?.Invoke(new AnalyzersDescriptor())?.Value);

		public AnalysisDescriptor CharFilters(Func<CharFiltersDescriptor, IPromise<ICharFilters>> selector) =>
			Assign(selector, (a, v) => a.CharFilters = v?.Invoke(new CharFiltersDescriptor())?.Value);

		public AnalysisDescriptor TokenFilters(Func<TokenFiltersDescriptor, IPromise<ITokenFilters>> selector) =>
			Assign(selector, (a, v) => a.TokenFilters = v?.Invoke(new TokenFiltersDescriptor())?.Value);

		public AnalysisDescriptor Tokenizers(Func<TokenizersDescriptor, IPromise<ITokenizers>> selector) =>
			Assign(selector, (a, v) => a.Tokenizers = v?.Invoke(new TokenizersDescriptor())?.Value);

		public AnalysisDescriptor Normalizers(Func<NormalizersDescriptor, IPromise<INormalizers>> selector) =>
			Assign(selector, (a, v) => a.Normalizers = v?.Invoke(new NormalizersDescriptor())?.Value);
	}
}
