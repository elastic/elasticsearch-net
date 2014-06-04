using System;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.Resolvers;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<DirectGenerator>))]
	public interface IDirectGenerator
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "prefix_len")]
		int? PrefixLen { get; set; }

		[JsonProperty(PropertyName = "suggest_mode")]
		[JsonConverter(typeof(StringEnumConverter))]
		SuggestModeOptions? SuggestMode { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		int? MinWordLen { get; set; }

		[JsonProperty(PropertyName = "max_edits")]
		int? MaxEdits { get; set; }

		[JsonProperty(PropertyName = "max_inspections")]
		decimal? MaxInspections { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		decimal? MinDocFrequency { get; set; }

		[JsonProperty(PropertyName = "max_term_freq")]
		decimal? MaxTermFrequency { get; set; }

		[JsonProperty(PropertyName = "pre_filter")]
		string PreFilter { get; set; }

		[JsonProperty(PropertyName = "post_filter")]
		string PostFilter { get; set; }
	}

	public class DirectGenerator : IDirectGenerator
	{
		public PropertyPathMarker Field { get; set; }
		public int? Size { get; set; }
		public int? PrefixLen { get; set; }
		public SuggestModeOptions? SuggestMode { get; set; }
		public int? MinWordLen { get; set; }
		public int? MaxEdits { get; set; }
		public decimal? MaxInspections { get; set; }
		public decimal? MinDocFrequency { get; set; }
		public decimal? MaxTermFrequency { get; set; }
		public string PreFilter { get; set; }
		public string PostFilter { get; set; }
	}

	public class DirectGeneratorDescriptor<T> : IDirectGenerator where T : class
	{
		private IDirectGenerator Self { get { return this; } }

		PropertyPathMarker IDirectGenerator.Field { get; set; }

		int? IDirectGenerator.Size { get; set; }

		int? IDirectGenerator.PrefixLen { get; set; }

		SuggestModeOptions? IDirectGenerator.SuggestMode { get; set; }

		int? IDirectGenerator.MinWordLen { get; set; }

		int? IDirectGenerator.MaxEdits { get; set; }

		decimal? IDirectGenerator.MaxInspections { get; set; }

		decimal? IDirectGenerator.MinDocFrequency { get; set; }

		decimal? IDirectGenerator.MaxTermFrequency { get; set; }

		string IDirectGenerator.PreFilter { get; set; }

		string IDirectGenerator.PostFilter { get; set; }

		public DirectGeneratorDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public DirectGeneratorDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public DirectGeneratorDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}

		public DirectGeneratorDescriptor<T> SuggestMode(SuggestModeOptions mode)
		{
			Self.SuggestMode = mode;
			return this;
		}

		public DirectGeneratorDescriptor<T> MinWordLength(int length)
		{
			Self.MinWordLen = length;
			return this;
		}

		public DirectGeneratorDescriptor<T> PrefixLength(int length)
		{
			Self.PrefixLen = length;
			return this;
		}

		public DirectGeneratorDescriptor<T> MaxEdits(int maxEdits)
		{
			Self.MaxEdits = maxEdits;
			return this;
		}

		public DirectGeneratorDescriptor<T> MaxInspections(decimal maxInspections)
		{
			Self.MaxInspections = maxInspections;
			return this;
		}

		public DirectGeneratorDescriptor<T> MinDocFrequency(decimal frequency)
		{
			Self.MinDocFrequency = frequency;
			return this;
		}

		public DirectGeneratorDescriptor<T> MaxTermFrequency(decimal frequency)
		{
			Self.MaxTermFrequency = frequency;
			return this;
		}

		public DirectGeneratorDescriptor<T> PreFilter(string analyzer)
		{
			Self.PreFilter = analyzer;
			return this;
		}

		public DirectGeneratorDescriptor<T> PostFilter(string analyzer)
		{
			Self.PostFilter = analyzer;
			return this;
		}

	}
}
