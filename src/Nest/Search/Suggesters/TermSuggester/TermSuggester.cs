using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TermSuggester>))]
	public interface ITermSuggester : ISuggester
	{
		[JsonIgnore]
		string Text { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty("suggest_mode")]
		[JsonConverter(typeof(StringEnumConverter))]
		SuggestMode? SuggestMode { get; set; }

		[JsonProperty("min_word_length")]
		int? MinWordLength { get; set; }

		[JsonProperty("max_edits")]
		int? MaxEdits { get; set; }

		[JsonProperty("max_inspections")]
		int? MaxInspections { get; set; }

		[JsonProperty("min_doc_freq")]
		decimal? MinDocFrequency { get; set; }

		[JsonProperty("max_term_freq")]
		decimal? MaxTermFrequency { get; set; }

		[JsonProperty("sort")]
		SuggestSort? Sort { get; set; }

		[JsonProperty("lowercase_terms")]
		bool? LowercaseTerms { get; set; }

		[JsonProperty("string_distance")]
		StringDistance? StringDistance { get; set; }
	}

	public class TermSuggester : SuggesterBase, ITermSuggester
	{
		public string Text { get; set; }
		public int? ShardSize { get; set; }
		public int? PrefixLength { get; set; }
		public SuggestMode? SuggestMode { get; set; }
		public int? MinWordLength { get; set; }
		public int? MaxEdits { get; set; }
		public int? MaxInspections { get; set; }
		public decimal? MinDocFrequency { get; set; }
		public decimal? MaxTermFrequency { get; set; }
		public SuggestSort? Sort { get; set; }
		public bool? LowercaseTerms { get; set; }
		public StringDistance? StringDistance { get; set; }
	}

	public class TermSuggesterDescriptor<T>
		: SuggestDescriptorBase<TermSuggesterDescriptor<T>, ITermSuggester, T>, ITermSuggester
		where T : class
	{
		string ITermSuggester.Text { get; set; }

		int? ITermSuggester.ShardSize { get; set; }

		int? ITermSuggester.PrefixLength { get; set; }

		SuggestMode? ITermSuggester.SuggestMode { get; set; }

		int? ITermSuggester.MinWordLength { get; set; }

		int? ITermSuggester.MaxEdits { get; set; }

		int? ITermSuggester.MaxInspections { get; set; }

		decimal? ITermSuggester.MinDocFrequency { get; set; }

		decimal? ITermSuggester.MaxTermFrequency { get; set; }

		SuggestSort? ITermSuggester.Sort { get; set; }

		bool? ITermSuggester.LowercaseTerms { get; set; }

		StringDistance? ITermSuggester.StringDistance { get; set; }

		public TermSuggesterDescriptor<T> Text(string text) => Assign(a => a.Text = text);

		public TermSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(a => a.ShardSize = shardSize);

		public TermSuggesterDescriptor<T> SuggestMode(SuggestMode? mode) => Assign(a => a.SuggestMode = mode);

		public TermSuggesterDescriptor<T> MinWordLength(int? length) => Assign(a => a.MinWordLength = length);

		public TermSuggesterDescriptor<T> PrefixLength(int? length) => Assign(a => a.PrefixLength = length);

		public TermSuggesterDescriptor<T> MaxEdits(int? maxEdits) => Assign(a => a.MaxEdits = maxEdits);

		public TermSuggesterDescriptor<T> MaxInspections(int? maxInspections) => Assign(a => a.MaxInspections = maxInspections);

		public TermSuggesterDescriptor<T> MinDocFrequency(decimal? frequency) => Assign(a => a.MinDocFrequency = frequency);

		public TermSuggesterDescriptor<T> MaxTermFrequency(decimal? frequency) => Assign(a => a.MaxTermFrequency = frequency);

		public TermSuggesterDescriptor<T> Sort(SuggestSort sort) => Assign(a => a.Sort = sort);

		public TermSuggesterDescriptor<T> LowercaseTerms(bool lowercaseTerms = true) => Assign(a => a.LowercaseTerms = lowercaseTerms);

		public TermSuggesterDescriptor<T> StringDistance(StringDistance distance) => Assign(a => a.StringDistance = distance);
	}
}
