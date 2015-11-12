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

	public class TermSuggester : Suggester, ITermSuggester
	{
		public int? PrefixLen { get; set; }
		public SuggestMode? SuggestMode { get; set; }
		public int? MinWordLen { get; set; }
		public int? MaxEdits { get; set; }
		public int? MaxInspections { get; set; }
		public decimal? MinDocFrequency { get; set; }
		public decimal? MaxTermFrequency { get; set; }
	}

	public class TermSuggestDescriptor<T> : BaseSuggestDescriptor<T>, ITermSuggester where T : class
	{
		private ITermSuggester Self => this;

		int? ITermSuggester.PrefixLen { get; set; }

		SuggestMode? ITermSuggester.SuggestMode { get; set; }

		int? ITermSuggester.MinWordLen { get; set; }

		int? ITermSuggester.MaxEdits { get; set; }

		int? ITermSuggester.MaxInspections { get; set; }

		decimal? ITermSuggester.MinDocFrequency { get; set; }

		decimal? ITermSuggester.MaxTermFrequency { get; set; }

		public TermSuggestDescriptor<T> Text(string text)
		{
			Self.Text = text;
			return this;
		}

		public TermSuggestDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public TermSuggestDescriptor<T> Field(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public TermSuggestDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}

		public TermSuggestDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}
		
		public TermSuggestDescriptor<T> ShardSize(int size)
		{
			Self.ShardSize = size;
			return this;
		}

		public TermSuggestDescriptor<T> SuggestMode(SuggestMode mode)
		{
			Self.SuggestMode = mode;
			return this;
		}

		public TermSuggestDescriptor<T> MinWordLength(int length)
		{
			Self.MinWordLen = length;
			return this;
		}

		public TermSuggestDescriptor<T> PrefixLength(int length)
		{
			Self.PrefixLen = length;
			return this;
		}

		public TermSuggestDescriptor<T> MaxEdits(int maxEdits)
		{
			Self.MaxEdits = maxEdits;
			return this;
		}

		public TermSuggestDescriptor<T> MaxInspections(int maxInspections)
		{
			Self.MaxInspections = maxInspections;
			return this;
		}

		public TermSuggestDescriptor<T> MinDocFrequency(decimal frequency)
		{
			Self.MinDocFrequency = frequency;
			return this;
		}

		public TermSuggestDescriptor<T> MaxTermFrequency(decimal frequency)
		{
			Self.MaxTermFrequency = frequency;
			return this;
		}

	}
}
