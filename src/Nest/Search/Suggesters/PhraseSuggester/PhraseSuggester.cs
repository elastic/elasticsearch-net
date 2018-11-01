using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PhraseSuggester>))]
	public interface IPhraseSuggester : ISuggester
	{
		[JsonProperty("collate")]
		IPhraseSuggestCollate Collate { get; set; }

		[JsonProperty("confidence")]
		double? Confidence { get; set; }

		[JsonProperty("direct_generator")]
		IEnumerable<IDirectGenerator> DirectGenerator { get; set; }

		[JsonProperty("force_unigrams")]
		bool? ForceUnigrams { get; set; }

		[JsonProperty("gram_size")]
		int? GramSize { get; set; }

		[JsonProperty("highlight")]
		IPhraseSuggestHighlight Highlight { get; set; }

		[JsonProperty("max_errors")]
		double? MaxErrors { get; set; }

		[JsonProperty("real_word_error_likelihood")]
		double? RealWordErrorLikelihood { get; set; }

		[JsonProperty("separator")]
		char? Separator { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("smoothing")]
		SmoothingModelContainer Smoothing { get; set; }

		[JsonIgnore]
		string Text { get; set; }

		[JsonProperty("token_limit")]
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

		public PhraseSuggesterDescriptor<T> Collate(Func<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate> selector) =>
			Assign(a => a.Collate = selector?.Invoke(new PhraseSuggestCollateDescriptor<T>()));

		public PhraseSuggesterDescriptor<T> Confidence(double? confidence) => Assign(a => a.Confidence = confidence);

		public PhraseSuggesterDescriptor<T> DirectGenerator(params Func<DirectGeneratorDescriptor<T>, IDirectGenerator>[] generators) =>
			Assign(a => a.DirectGenerator = generators.Select(g => g(new DirectGeneratorDescriptor<T>())).ToList());

		public PhraseSuggesterDescriptor<T> ForceUnigrams(bool? forceUnigrams = true) => Assign(a => a.ForceUnigrams = forceUnigrams);

		public PhraseSuggesterDescriptor<T> GramSize(int? gramSize) => Assign(a => a.GramSize = gramSize);

		public PhraseSuggesterDescriptor<T> Highlight(Func<PhraseSuggestHighlightDescriptor, IPhraseSuggestHighlight> selector) =>
			Assign(a => a.Highlight = selector?.Invoke(new PhraseSuggestHighlightDescriptor()));

		public PhraseSuggesterDescriptor<T> MaxErrors(double? maxErrors) => Assign(a => a.MaxErrors = maxErrors);

		public PhraseSuggesterDescriptor<T> RealWordErrorLikelihood(double? realWordErrorLikelihood) =>
			Assign(a => a.RealWordErrorLikelihood = realWordErrorLikelihood);

		public PhraseSuggesterDescriptor<T> Separator(char? separator) => Assign(a => a.Separator = separator);

		public PhraseSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(a => a.ShardSize = shardSize);

		public PhraseSuggesterDescriptor<T> Smoothing(Func<SmoothingModelContainerDescriptor, SmoothingModelContainer> selector) =>
			Assign(a => a.Smoothing = selector?.Invoke(new SmoothingModelContainerDescriptor()));

		public PhraseSuggesterDescriptor<T> Text(string text) => Assign(a => a.Text = text);

		public PhraseSuggesterDescriptor<T> TokenLimit(int? tokenLimit) => Assign(a => a.TokenLimit = tokenLimit);
	}
}
