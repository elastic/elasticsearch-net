using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TermSuggester))]
	public interface ITermSuggester : ISuggester
	{
		[DataMember(Name = "lowercase_terms")]
		bool? LowercaseTerms { get; set; }

		[DataMember(Name = "max_edits")]
		int? MaxEdits { get; set; }

		[DataMember(Name = "max_inspections")]
		int? MaxInspections { get; set; }

		[DataMember(Name = "max_term_freq")]
		decimal? MaxTermFrequency { get; set; }

		[DataMember(Name = "min_doc_freq")]
		decimal? MinDocFrequency { get; set; }

		[DataMember(Name = "min_word_length")]
		int? MinWordLength { get; set; }

		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		[DataMember(Name = "shard_size")]
		int? ShardSize { get; set; }

		[DataMember(Name = "sort")]
		SuggestSort? Sort { get; set; }

		[DataMember(Name = "string_distance")]
		StringDistance? StringDistance { get; set; }

		[DataMember(Name = "suggest_mode")]

		SuggestMode? SuggestMode { get; set; }

		[IgnoreDataMember]
		string Text { get; set; }
	}

	public class TermSuggester : SuggesterBase, ITermSuggester
	{
		public bool? LowercaseTerms { get; set; }
		public int? MaxEdits { get; set; }
		public int? MaxInspections { get; set; }
		public decimal? MaxTermFrequency { get; set; }
		public decimal? MinDocFrequency { get; set; }
		public int? MinWordLength { get; set; }
		public int? PrefixLength { get; set; }
		public int? ShardSize { get; set; }
		public SuggestSort? Sort { get; set; }
		public StringDistance? StringDistance { get; set; }
		public SuggestMode? SuggestMode { get; set; }
		public string Text { get; set; }
	}

	public class TermSuggesterDescriptor<T>
		: SuggestDescriptorBase<TermSuggesterDescriptor<T>, ITermSuggester, T>, ITermSuggester
		where T : class
	{
		bool? ITermSuggester.LowercaseTerms { get; set; }

		int? ITermSuggester.MaxEdits { get; set; }

		int? ITermSuggester.MaxInspections { get; set; }

		decimal? ITermSuggester.MaxTermFrequency { get; set; }

		decimal? ITermSuggester.MinDocFrequency { get; set; }

		int? ITermSuggester.MinWordLength { get; set; }

		int? ITermSuggester.PrefixLength { get; set; }

		int? ITermSuggester.ShardSize { get; set; }

		SuggestSort? ITermSuggester.Sort { get; set; }

		StringDistance? ITermSuggester.StringDistance { get; set; }

		SuggestMode? ITermSuggester.SuggestMode { get; set; }
		string ITermSuggester.Text { get; set; }

		public TermSuggesterDescriptor<T> Text(string text) => Assign(text, (a, v) => a.Text = v);

		public TermSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);

		public TermSuggesterDescriptor<T> SuggestMode(SuggestMode? mode) => Assign(mode, (a, v) => a.SuggestMode = v);

		public TermSuggesterDescriptor<T> MinWordLength(int? length) => Assign(length, (a, v) => a.MinWordLength = v);

		public TermSuggesterDescriptor<T> PrefixLength(int? length) => Assign(length, (a, v) => a.PrefixLength = v);

		public TermSuggesterDescriptor<T> MaxEdits(int? maxEdits) => Assign(maxEdits, (a, v) => a.MaxEdits = v);

		public TermSuggesterDescriptor<T> MaxInspections(int? maxInspections) => Assign(maxInspections, (a, v) => a.MaxInspections = v);

		public TermSuggesterDescriptor<T> MinDocFrequency(decimal? frequency) => Assign(frequency, (a, v) => a.MinDocFrequency = v);

		public TermSuggesterDescriptor<T> MaxTermFrequency(decimal? frequency) => Assign(frequency, (a, v) => a.MaxTermFrequency = v);

		public TermSuggesterDescriptor<T> Sort(SuggestSort? sort) => Assign(sort, (a, v) => a.Sort = v);

		public TermSuggesterDescriptor<T> LowercaseTerms(bool? lowercaseTerms = true) => Assign(lowercaseTerms, (a, v) => a.LowercaseTerms = v);

		public TermSuggesterDescriptor<T> StringDistance(StringDistance? distance) => Assign(distance, (a, v) => a.StringDistance = v);
	}
}
