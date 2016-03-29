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
		[JsonIgnore]
		string Text { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("gram_size")]
		int? GramSize { get; set; }

		[JsonProperty("real_word_error_likelihood")]
		double? RealWordErrorLikelihood { get; set; }

		[JsonProperty("confidence")]
		double? Confidence { get; set; }

		[JsonProperty("max_errors")]
		double? MaxErrors { get; set; }

		[JsonProperty("separator")]
		char? Separator { get; set; }

		[JsonProperty("direct_generator")]
		IEnumerable<IDirectGenerator> DirectGenerator { get; set; }

		[JsonProperty("highlight")]
		IPhraseSuggestHighlight Highlight { get; set; }

		[JsonProperty("collate")]
		IPhraseSuggestCollate Collate { get; set; }
	}

	public class PhraseSuggester : SuggesterBase, IPhraseSuggester
	{
		public string Text { get; set; }
		public int? ShardSize { get; set; }
		public int? GramSize { get; set; }
		public double? RealWordErrorLikelihood { get; set; }
		public double? Confidence { get; set; }
		public double? MaxErrors { get; set; }
		public char? Separator { get; set; }
		public IEnumerable<IDirectGenerator> DirectGenerator { get; set; }
		public IPhraseSuggestHighlight Highlight { get; set; }
		public IPhraseSuggestCollate Collate { get; set; }
	}

	public class PhraseSuggesterDescriptor<T> : SuggestDescriptorBase<PhraseSuggesterDescriptor<T>, IPhraseSuggester, T>, IPhraseSuggester
		where T : class
	{
		string IPhraseSuggester.Text { get; set; }
		int? IPhraseSuggester.ShardSize { get; set; }
		int? IPhraseSuggester.GramSize { get; set; }
		double? IPhraseSuggester.RealWordErrorLikelihood { get; set; }
		double? IPhraseSuggester.Confidence { get; set; }
		double? IPhraseSuggester.MaxErrors { get; set; }
		char? IPhraseSuggester.Separator { get; set; }
		IEnumerable<IDirectGenerator> IPhraseSuggester.DirectGenerator { get; set; }
		IPhraseSuggestHighlight IPhraseSuggester.Highlight { get; set; }
		IPhraseSuggestCollate IPhraseSuggester.Collate { get; set; }

		public PhraseSuggesterDescriptor<T> Text(string text) => Assign(a => a.Text = text);

		public PhraseSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(a => a.ShardSize = shardSize);

		public PhraseSuggesterDescriptor<T> GramSize(int? gramSize) => Assign(a => a.GramSize = gramSize);

		public PhraseSuggesterDescriptor<T> Confidence(double? confidence) => Assign(a => a.Confidence = confidence);

		public PhraseSuggesterDescriptor<T> MaxErrors(double? maxErrors) => Assign(a => a.MaxErrors = maxErrors);

		public PhraseSuggesterDescriptor<T> Separator(char? separator) => Assign(a => a.Separator = separator);

		public PhraseSuggesterDescriptor<T> DirectGenerator(params Func<DirectGeneratorDescriptor<T>, IDirectGenerator>[] generators) =>
			Assign(a=>a.DirectGenerator = generators.Select(g => g(new DirectGeneratorDescriptor<T>())).ToList());

		public PhraseSuggesterDescriptor<T> RealWordErrorLikelihood(double? realWordErrorLikelihood) =>
			Assign(a => a.RealWordErrorLikelihood = realWordErrorLikelihood);

		public PhraseSuggesterDescriptor<T> Highlight(Func<PhraseSuggestHighlightDescriptor, IPhraseSuggestHighlight> selector) =>
			Assign(a=> a.Highlight = selector?.Invoke(new PhraseSuggestHighlightDescriptor()));

		public PhraseSuggesterDescriptor<T> Collate(Func<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate> selector) =>
			Assign(a => a.Collate = selector?.Invoke(new PhraseSuggestCollateDescriptor<T>()));
	}
}
