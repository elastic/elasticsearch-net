using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PhraseSuggester))]
	public interface IPhraseSuggester : ISuggester
	{
		[DataMember(Name = "collate")]
		IPhraseSuggestCollate Collate { get; set; }

		[DataMember(Name = "confidence")]
		double? Confidence { get; set; }

		[DataMember(Name = "direct_generator")]
		IEnumerable<IDirectGenerator> DirectGenerator { get; set; }

		[DataMember(Name = "force_unigrams")]
		bool? ForceUnigrams { get; set; }

		[DataMember(Name = "gram_size")]
		int? GramSize { get; set; }

		[DataMember(Name = "highlight")]
		IPhraseSuggestHighlight Highlight { get; set; }

		[DataMember(Name = "max_errors")]
		double? MaxErrors { get; set; }

		[DataMember(Name = "real_word_error_likelihood")]
		double? RealWordErrorLikelihood { get; set; }

		[DataMember(Name = "separator")]
		char? Separator { get; set; }

		[DataMember(Name = "shard_size")]
		int? ShardSize { get; set; }

		[DataMember(Name = "smoothing")]
		SmoothingModelContainer Smoothing { get; set; }

		[IgnoreDataMember]
		string Text { get; set; }

		[DataMember(Name = "token_limit")]
		int? TokenLimit { get; set; }
	}

	public class PhraseSuggester : SuggesterBase, IPhraseSuggester
	{
		public IPhraseSuggestCollate Collate { get; set; }
		public double? Confidence { get; set; }
		public IEnumerable<IDirectGenerator> DirectGenerator { get; set; }
		public bool? ForceUnigrams { get; set; }
		public int? GramSize { get; set; }
		public IPhraseSuggestHighlight Highlight { get; set; }
		public double? MaxErrors { get; set; }
		public double? RealWordErrorLikelihood { get; set; }
		public char? Separator { get; set; }
		public int? ShardSize { get; set; }
		public SmoothingModelContainer Smoothing { get; set; }
		public string Text { get; set; }
		public int? TokenLimit { get; set; }
	}

	public class PhraseSuggesterDescriptor<T> : SuggestDescriptorBase<PhraseSuggesterDescriptor<T>, IPhraseSuggester, T>, IPhraseSuggester
		where T : class
	{
		IPhraseSuggestCollate IPhraseSuggester.Collate { get; set; }
		double? IPhraseSuggester.Confidence { get; set; }
		IEnumerable<IDirectGenerator> IPhraseSuggester.DirectGenerator { get; set; }
		bool? IPhraseSuggester.ForceUnigrams { get; set; }
		int? IPhraseSuggester.GramSize { get; set; }
		IPhraseSuggestHighlight IPhraseSuggester.Highlight { get; set; }
		double? IPhraseSuggester.MaxErrors { get; set; }
		double? IPhraseSuggester.RealWordErrorLikelihood { get; set; }
		char? IPhraseSuggester.Separator { get; set; }
		int? IPhraseSuggester.ShardSize { get; set; }
		SmoothingModelContainer IPhraseSuggester.Smoothing { get; set; }
		string IPhraseSuggester.Text { get; set; }
		int? IPhraseSuggester.TokenLimit { get; set; }

		public PhraseSuggesterDescriptor<T> Text(string text) => Assign(text, (a, v) => a.Text = v);

		public PhraseSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);

		public PhraseSuggesterDescriptor<T> GramSize(int? gramSize) => Assign(gramSize, (a, v) => a.GramSize = v);

		public PhraseSuggesterDescriptor<T> Confidence(double? confidence) => Assign(confidence, (a, v) => a.Confidence = v);

		public PhraseSuggesterDescriptor<T> MaxErrors(double? maxErrors) => Assign(maxErrors, (a, v) => a.MaxErrors = v);

		public PhraseSuggesterDescriptor<T> Separator(char? separator) => Assign(separator, (a, v) => a.Separator = v);

		public PhraseSuggesterDescriptor<T> DirectGenerator(params Func<DirectGeneratorDescriptor<T>, IDirectGenerator>[] generators) =>
			Assign(generators.Select(g => g(new DirectGeneratorDescriptor<T>())).ToList(), (a, v) => a.DirectGenerator = v);

		public PhraseSuggesterDescriptor<T> RealWordErrorLikelihood(double? realWordErrorLikelihood) =>
			Assign(realWordErrorLikelihood, (a, v) => a.RealWordErrorLikelihood = v);

		public PhraseSuggesterDescriptor<T> Highlight(Func<PhraseSuggestHighlightDescriptor, IPhraseSuggestHighlight> selector) =>
			Assign(selector, (a, v) => a.Highlight = v?.Invoke(new PhraseSuggestHighlightDescriptor()));

		public PhraseSuggesterDescriptor<T> Collate(Func<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate> selector) =>
			Assign(selector, (a, v) => a.Collate = v?.Invoke(new PhraseSuggestCollateDescriptor<T>()));

		public PhraseSuggesterDescriptor<T> Smoothing(Func<SmoothingModelContainerDescriptor, SmoothingModelContainer> selector) =>
			Assign(selector, (a, v) => a.Smoothing = v?.Invoke(new SmoothingModelContainerDescriptor()));

		public PhraseSuggesterDescriptor<T> TokenLimit(int? tokenLimit) => Assign(tokenLimit, (a, v) => a.TokenLimit = v);

		public PhraseSuggesterDescriptor<T> ForceUnigrams(bool? forceUnigrams = true) => Assign(forceUnigrams, (a, v) => a.ForceUnigrams = v);
	}
}
