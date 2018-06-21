using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IFieldCapabilitiesResponse : IResponse
	{
		[JsonProperty("fields")]
		FieldCapabilitiesFields Fields { get; }
	}

	public class FieldCapabilitiesResponse : ResponseBase, IFieldCapabilitiesResponse
	{
		public ShardStatistics Shards { get; internal set; }
		public FieldCapabilitiesFields Fields { get; internal set; }
	}

	[JsonConverter(typeof(FieldCapabilitiesFields.Converter))]
	public class FieldCapabilitiesFields : ResolvableDictionaryProxy<Field, FieldTypes>
	{
		internal FieldCapabilitiesFields(IConnectionConfigurationValues c, IReadOnlyDictionary<Field, FieldTypes> b) : base(c, b) { }

		internal class Converter : ResolvableDictionaryJsonConverterBase<FieldCapabilitiesFields, Field, FieldTypes>
		{
			protected override FieldCapabilitiesFields Create(IConnectionSettingsValues s, Dictionary<Field, FieldTypes> d) =>
				new FieldCapabilitiesFields(s, d);
		}
	}

	public class FieldTypes : IsADictionaryBase<string, FieldCapabilities>
	{
		public FieldCapabilities All => this.BackingDictionary.TryGetValue("_all", out var f) ? f : null;
		public FieldCapabilities Id => this.BackingDictionary.TryGetValue("_id", out var f) ? f : null;
		public FieldCapabilities Uid => this.BackingDictionary.TryGetValue("_uid", out var f) ? f : null;
		public FieldCapabilities Type => this.BackingDictionary.TryGetValue("_type", out var f) ? f : null;
		public FieldCapabilities Index => this.BackingDictionary.TryGetValue("_index", out var f) ? f : null;
		public FieldCapabilities Parent => this.BackingDictionary.TryGetValue("_parent", out var f) ? f : null;
		public FieldCapabilities Version => this.BackingDictionary.TryGetValue("_version", out var f) ? f : null;
		public FieldCapabilities Routing => this.BackingDictionary.TryGetValue("_routing", out var f) ? f : null;
		public FieldCapabilities Source => this.BackingDictionary.TryGetValue("_source", out var f) ? f : null;
		public FieldCapabilities FieldNames => this.BackingDictionary.TryGetValue("_field_names", out var f) ? f : null;

		public FieldCapabilities Keyword => this.BackingDictionary.TryGetValue("keyword", out var f) ? f : null;
		public FieldCapabilities Text => this.BackingDictionary.TryGetValue("text", out var f) ? f : null;
		public FieldCapabilities GeoPoint => this.BackingDictionary.TryGetValue("geo_point", out var f) ? f : null;
		public FieldCapabilities GeoShape => this.BackingDictionary.TryGetValue("geo_shape", out var f) ? f : null;
		public FieldCapabilities Attachment => this.BackingDictionary.TryGetValue("attachment", out var f) ? f : null;
		public FieldCapabilities Ip => this.BackingDictionary.TryGetValue("ip", out var f) ? f : null;
		public FieldCapabilities Binary => this.BackingDictionary.TryGetValue("binary", out var f) ? f : null;
		public FieldCapabilities Date => this.BackingDictionary.TryGetValue("date", out var f) ? f : null;
		public FieldCapabilities Boolean => this.BackingDictionary.TryGetValue("boolean", out var f) ? f : null;
		public FieldCapabilities Completion => this.BackingDictionary.TryGetValue("completion", out var f) ? f : null;
		public FieldCapabilities Murmur3 => this.BackingDictionary.TryGetValue("murmur3", out var f) ? f : null;
		public FieldCapabilities TokenCount => this.BackingDictionary.TryGetValue("token_count", out var f) ? f : null;
		public FieldCapabilities Percolator => this.BackingDictionary.TryGetValue("percolator", out var f) ? f : null;
		public FieldCapabilities Integer => this.BackingDictionary.TryGetValue("integer", out var f) ? f : null;
		public FieldCapabilities Long => this.BackingDictionary.TryGetValue("long", out var f) ? f : null;
		public FieldCapabilities Short => this.BackingDictionary.TryGetValue("short", out var f) ? f : null;
		public FieldCapabilities Byte => this.BackingDictionary.TryGetValue("byte", out var f) ? f : null;
		public FieldCapabilities Float => this.BackingDictionary.TryGetValue("float", out var f) ? f : null;
		public FieldCapabilities HalfFloat => this.BackingDictionary.TryGetValue("half_float", out var f) ? f : null;
		public FieldCapabilities ScaledFloat => this.BackingDictionary.TryGetValue("scaled_float", out var f) ? f : null;
		public FieldCapabilities Double => this.BackingDictionary.TryGetValue("double", out var f) ? f : null;
		public FieldCapabilities IntegerRange => this.BackingDictionary.TryGetValue("integer_range", out var f) ? f : null;
		public FieldCapabilities FloatRange => this.BackingDictionary.TryGetValue("float_range", out var f) ? f : null;
		public FieldCapabilities LongRange => this.BackingDictionary.TryGetValue("long_range", out var f) ? f : null;
		public FieldCapabilities DoubleRange => this.BackingDictionary.TryGetValue("double_range", out var f) ? f : null;
		public FieldCapabilities DateRange => this.BackingDictionary.TryGetValue("date_range", out var f) ? f : null;
	}

	public class FieldCapabilities
	{
		[JsonProperty("searchable")]
		public bool Searchable { get; internal set; }

		[JsonProperty("aggregatable")]
		public bool Aggregatable { get; internal set; }

		[JsonProperty("indices")]
		public Indices Indices { get; internal set; }

		[JsonProperty("non_searchable_indices")]
		public Indices NonSearchableIndices { get; internal set; }

		[JsonProperty("non_aggregatable_indices")]
		public Indices NonAggregatableIndices { get; internal set; }

	}
}
