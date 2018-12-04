using System.Runtime.Serialization;
using Elasticsearch.Net;
using Utf8Json;

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

		public TermSuggesterDescriptor<T> Text(string text) => Assign(a => a.Text = text);

		public TermSuggesterDescriptor<T> ShardSize(int? shardSize) => Assign(a => a.ShardSize = shardSize);

		public TermSuggesterDescriptor<T> SuggestMode(SuggestMode? mode) => Assign(a => a.SuggestMode = mode);

		public TermSuggesterDescriptor<T> MinWordLength(int? length) => Assign(a => a.MinWordLength = length);

		public TermSuggesterDescriptor<T> PrefixLength(int? length) => Assign(a => a.PrefixLength = length);

		public TermSuggesterDescriptor<T> MaxEdits(int? maxEdits) => Assign(a => a.MaxEdits = maxEdits);

		public TermSuggesterDescriptor<T> MaxInspections(int? maxInspections) => Assign(a => a.MaxInspections = maxInspections);

		public TermSuggesterDescriptor<T> MinDocFrequency(decimal? frequency) => Assign(a => a.MinDocFrequency = frequency);

		public TermSuggesterDescriptor<T> MaxTermFrequency(decimal? frequency) => Assign(a => a.MaxTermFrequency = frequency);

		public TermSuggesterDescriptor<T> Sort(SuggestSort? sort) => Assign(a => a.Sort = sort);

		public TermSuggesterDescriptor<T> LowercaseTerms(bool? lowercaseTerms = true) => Assign(a => a.LowercaseTerms = lowercaseTerms);

		public TermSuggesterDescriptor<T> StringDistance(StringDistance? distance) => Assign(a => a.StringDistance = distance);
	}
}
