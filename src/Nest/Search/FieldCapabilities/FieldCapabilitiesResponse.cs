using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IFieldCapabilitiesResponse : IResponse
	{
		[JsonProperty("fields")]
		IReadOnlyDictionary<string, FieldTypes> Fields { get; }
	}

	public class FieldCapabilitiesResponse : ResponseBase, IFieldCapabilitiesResponse
	{
		public ShardStatistics Shards { get; internal set; }
		public IReadOnlyDictionary<string, FieldTypes> Fields { get; internal set; } = EmptyReadOnly<string, FieldTypes>.Dictionary;
	}

	public class FieldTypes : IsADictionaryBase<string, FieldCapabilities>
	{
		public FieldCapabilities All => this.BackingDictionary.TryGetValue("_all", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Id => this.BackingDictionary.TryGetValue("_id", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Uid => this.BackingDictionary.TryGetValue("_uid", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Type => this.BackingDictionary.TryGetValue("_type", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Index => this.BackingDictionary.TryGetValue("_index", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Parent => this.BackingDictionary.TryGetValue("_parent", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Version => this.BackingDictionary.TryGetValue("_version", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Routing => this.BackingDictionary.TryGetValue("_routing", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Source => this.BackingDictionary.TryGetValue("_source", out FieldCapabilities f) ? f : null;
		public FieldCapabilities FieldNames => this.BackingDictionary.TryGetValue("_field_names", out FieldCapabilities f) ? f : null;

		public FieldCapabilities Keyword => this.BackingDictionary.TryGetValue("keyword", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Text => this.BackingDictionary.TryGetValue("text", out FieldCapabilities f) ? f : null;
		public FieldCapabilities GeoPoint => this.BackingDictionary.TryGetValue("geo_point", out FieldCapabilities f) ? f : null;
		public FieldCapabilities GeoShape => this.BackingDictionary.TryGetValue("geo_shape", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Attachment => this.BackingDictionary.TryGetValue("attachment", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Ip => this.BackingDictionary.TryGetValue("ip", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Binary => this.BackingDictionary.TryGetValue("binary", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Date => this.BackingDictionary.TryGetValue("date", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Boolean => this.BackingDictionary.TryGetValue("boolean", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Completion => this.BackingDictionary.TryGetValue("completion", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Murmur3 => this.BackingDictionary.TryGetValue("murmur3", out FieldCapabilities f) ? f : null;
		public FieldCapabilities TokenCount => this.BackingDictionary.TryGetValue("token_count", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Percolator => this.BackingDictionary.TryGetValue("percolator", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Integer => this.BackingDictionary.TryGetValue("integer", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Long => this.BackingDictionary.TryGetValue("long", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Short => this.BackingDictionary.TryGetValue("short", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Byte => this.BackingDictionary.TryGetValue("byte", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Float => this.BackingDictionary.TryGetValue("float", out FieldCapabilities f) ? f : null;
		public FieldCapabilities HalfFloat => this.BackingDictionary.TryGetValue("half_float", out FieldCapabilities f) ? f : null;
		public FieldCapabilities ScaledFloat => this.BackingDictionary.TryGetValue("scaled_float", out FieldCapabilities f) ? f : null;
		public FieldCapabilities Double => this.BackingDictionary.TryGetValue("double", out FieldCapabilities f) ? f : null;
		public FieldCapabilities IntegerRange => this.BackingDictionary.TryGetValue("integer_range", out FieldCapabilities f) ? f : null;
		public FieldCapabilities FloatRange => this.BackingDictionary.TryGetValue("float_range", out FieldCapabilities f) ? f : null;
		public FieldCapabilities LongRange => this.BackingDictionary.TryGetValue("long_range", out FieldCapabilities f) ? f : null;
		public FieldCapabilities DoubleRange => this.BackingDictionary.TryGetValue("double_range", out FieldCapabilities f) ? f : null;
		public FieldCapabilities DateRange => this.BackingDictionary.TryGetValue("date_range", out FieldCapabilities f) ? f : null;
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
