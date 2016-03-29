using System;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DirectGenerator>))]
	public interface IDirectGenerator
	{
		[JsonProperty(PropertyName = "field")]
		Field Field { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty(PropertyName = "suggest_mode")]
		[JsonConverter(typeof(StringEnumConverter))]
		SuggestMode? SuggestMode { get; set; }

		[JsonProperty(PropertyName = "min_word_length")]
		int? MinWordLength { get; set; }

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
		public Field Field { get; set; }
		public int? Size { get; set; }
		public int? PrefixLength { get; set; }
		public SuggestMode? SuggestMode { get; set; }
		public int? MinWordLength { get; set; }
		public int? MaxEdits { get; set; }
		public decimal? MaxInspections { get; set; }
		public decimal? MinDocFrequency { get; set; }
		public decimal? MaxTermFrequency { get; set; }
		public string PreFilter { get; set; }
		public string PostFilter { get; set; }
	}

	public class DirectGeneratorDescriptor<T> : DescriptorBase<DirectGeneratorDescriptor<T>, IDirectGenerator>, IDirectGenerator
		where T : class
	{
		Field IDirectGenerator.Field { get; set; }
		int? IDirectGenerator.Size { get; set; }
		int? IDirectGenerator.PrefixLength { get; set; }
		SuggestMode? IDirectGenerator.SuggestMode { get; set; }
		int? IDirectGenerator.MinWordLength { get; set; }
		int? IDirectGenerator.MaxEdits { get; set; }
		decimal? IDirectGenerator.MaxInspections { get; set; }
		decimal? IDirectGenerator.MinDocFrequency { get; set; }
		decimal? IDirectGenerator.MaxTermFrequency { get; set; }
		string IDirectGenerator.PreFilter { get; set; }
		string IDirectGenerator.PostFilter { get; set; }

		public DirectGeneratorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public DirectGeneratorDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		public DirectGeneratorDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public DirectGeneratorDescriptor<T> SuggestMode(SuggestMode? mode) => Assign(a => a.SuggestMode = mode);

		public DirectGeneratorDescriptor<T> MinWordLength(int? length) => Assign(a => a.MinWordLength = length);

		public DirectGeneratorDescriptor<T> PrefixLength(int? length) => Assign(a => a.PrefixLength = length);

		public DirectGeneratorDescriptor<T> MaxEdits(int? maxEdits) => Assign(a => a.MaxEdits = maxEdits);

		public DirectGeneratorDescriptor<T> MaxInspections(decimal? maxInspections) => Assign(a => a.MaxInspections = maxInspections);

		public DirectGeneratorDescriptor<T> MinDocFrequency(decimal? frequency) => Assign(a => a.MinDocFrequency = frequency);

		public DirectGeneratorDescriptor<T> MaxTermFrequency(decimal? frequency) => Assign(a => a.MaxTermFrequency = frequency);

		public DirectGeneratorDescriptor<T> PreFilter(string preFilter) => Assign(a => a.PreFilter = preFilter);

		public DirectGeneratorDescriptor<T> PostFilter(string postFilter) => Assign(a => a.PostFilter = postFilter);

	}
}
