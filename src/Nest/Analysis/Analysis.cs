using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(Analysis))]
	public interface IAnalysis
	{
		[DataMember(Name ="analyzer")]
		IAnalyzers Analyzers { get; set; }

		[DataMember(Name ="char_filter")]
		ICharFilters CharFilters { get; set; }

		[DataMember(Name ="normalizer")]
		INormalizers Normalizers { get; set; }

		[DataMember(Name ="filter")]
		ITokenFilters TokenFilters { get; set; }

		[DataMember(Name ="tokenizer")]
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

	[DataContract]
	public class AnalysisDescriptor : DescriptorBase<AnalysisDescriptor, IAnalysis>, IAnalysis
	{
		IAnalyzers IAnalysis.Analyzers { get; set; }
		ICharFilters IAnalysis.CharFilters { get; set; }
		INormalizers IAnalysis.Normalizers { get; set; }
		ITokenFilters IAnalysis.TokenFilters { get; set; }
		ITokenizers IAnalysis.Tokenizers { get; set; }

		public AnalysisDescriptor Analyzers(Func<AnalyzersDescriptor, IPromise<IAnalyzers>> selector) =>
			Assign(a => a.Analyzers = selector?.Invoke(new AnalyzersDescriptor())?.Value);

		public AnalysisDescriptor CharFilters(Func<CharFiltersDescriptor, IPromise<ICharFilters>> selector) =>
			Assign(a => a.CharFilters = selector?.Invoke(new CharFiltersDescriptor())?.Value);

		public AnalysisDescriptor TokenFilters(Func<TokenFiltersDescriptor, IPromise<ITokenFilters>> selector) =>
			Assign(a => a.TokenFilters = selector?.Invoke(new TokenFiltersDescriptor())?.Value);

		public AnalysisDescriptor Tokenizers(Func<TokenizersDescriptor, IPromise<ITokenizers>> selector) =>
			Assign(a => a.Tokenizers = selector?.Invoke(new TokenizersDescriptor())?.Value);

		public AnalysisDescriptor Normalizers(Func<NormalizersDescriptor, IPromise<INormalizers>> selector) =>
			Assign(a => a.Normalizers = selector?.Invoke(new NormalizersDescriptor())?.Value);
	}
}
