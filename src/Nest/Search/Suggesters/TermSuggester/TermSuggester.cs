using System;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TermSuggester>))]
	public interface ITermSuggester : ISuggester
	{
		[JsonProperty(PropertyName = "prefix_len")]
		int? PrefixLen { get; set; }

		[JsonProperty(PropertyName = "suggest_mode")]
		[JsonConverter(typeof(StringEnumConverter))]
		SuggestMode? SuggestMode { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		int? MinWordLen { get; set; }

		[JsonProperty(PropertyName = "max_edits")]
		int? MaxEdits { get; set; }

		[JsonProperty(PropertyName = "max_inspections")]
		int? MaxInspections { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		decimal? MinDocFrequency { get; set; }

		[JsonProperty(PropertyName = "max_term_freq")]
		decimal? MaxTermFrequency { get; set; }
	}

	public class TermSuggester : SuggesterBase, ITermSuggester
	{
		public int? PrefixLen { get; set; }
		public SuggestMode? SuggestMode { get; set; }
		public int? MinWordLen { get; set; }
		public int? MaxEdits { get; set; }
		public int? MaxInspections { get; set; }
		public decimal? MinDocFrequency { get; set; }
		public decimal? MaxTermFrequency { get; set; }
	}

	public class TermSuggesterDescriptor<T> : SuggesterBaseDescriptor<TermSuggesterDescriptor<T>, ITermSuggester, T>, ITermSuggester 
		where T : class
	{
		int? ITermSuggester.PrefixLen { get; set; }

		SuggestMode? ITermSuggester.SuggestMode { get; set; }

		int? ITermSuggester.MinWordLen { get; set; }

		int? ITermSuggester.MaxEdits { get; set; }

		int? ITermSuggester.MaxInspections { get; set; }

		decimal? ITermSuggester.MinDocFrequency { get; set; }

		decimal? ITermSuggester.MaxTermFrequency { get; set; }

		public TermSuggesterDescriptor<T> SuggestMode(SuggestMode mode) => Assign(a => a.SuggestMode = mode);

		public TermSuggesterDescriptor<T> MinWordLength(int length) => Assign(a => a.MinWordLen = length);

		public TermSuggesterDescriptor<T> PrefixLength(int length) => Assign(a => a.PrefixLen = length);

		public TermSuggesterDescriptor<T> MaxEdits(int maxEdits) => Assign(a => a.MaxEdits = maxEdits);

		public TermSuggesterDescriptor<T> MaxInspections(int maxInspections) => Assign(a => a.MaxInspections = maxInspections);

		public TermSuggesterDescriptor<T> MinDocFrequency(decimal frequency) => Assign(a => a.MinDocFrequency = frequency);

		public TermSuggesterDescriptor<T> MaxTermFrequency(decimal frequency) => Assign(a => a.MaxTermFrequency = frequency);

	}
}
