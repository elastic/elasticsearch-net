// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	public class FieldCapabilitiesResponse : ResponseBase
	{
		[DataMember(Name = "fields")]
		public FieldCapabilitiesFields Fields { get; internal set; }
		//public ShardStatistics Shards { get; internal set; }
	}

	[JsonFormatter(typeof(Converter))]
	public class FieldCapabilitiesFields : ResolvableDictionaryProxy<Field, FieldTypes>
	{
		internal FieldCapabilitiesFields(IConnectionConfigurationValues c, IReadOnlyDictionary<Field, FieldTypes> b) : base(c, b) { }

		internal class Converter : ResolvableDictionaryFormatterBase<FieldCapabilitiesFields, Field, FieldTypes>
		{
			protected override FieldCapabilitiesFields Create(IConnectionSettingsValues s, Dictionary<Field, FieldTypes> d) =>
				new FieldCapabilitiesFields(s, d);
		}
	}

	public class FieldTypes : IsADictionaryBase<string, FieldCapabilities>
	{
		public FieldCapabilities All => BackingDictionary.TryGetValue("_all", out var f) ? f : null;
		public FieldCapabilities Attachment => BackingDictionary.TryGetValue("attachment", out var f) ? f : null;
		public FieldCapabilities Binary => BackingDictionary.TryGetValue("binary", out var f) ? f : null;
		public FieldCapabilities Boolean => BackingDictionary.TryGetValue("boolean", out var f) ? f : null;
		public FieldCapabilities Byte => BackingDictionary.TryGetValue("byte", out var f) ? f : null;
		public FieldCapabilities Completion => BackingDictionary.TryGetValue("completion", out var f) ? f : null;
		public FieldCapabilities Date => BackingDictionary.TryGetValue("date", out var f) ? f : null;
		public FieldCapabilities DateNanos => BackingDictionary.TryGetValue("date_nanos", out var f) ? f : null;
		public FieldCapabilities DateRange => BackingDictionary.TryGetValue("date_range", out var f) ? f : null;
		public FieldCapabilities Double => BackingDictionary.TryGetValue("double", out var f) ? f : null;
		public FieldCapabilities DoubleRange => BackingDictionary.TryGetValue("double_range", out var f) ? f : null;
		public FieldCapabilities FieldNames => BackingDictionary.TryGetValue("_field_names", out var f) ? f : null;
		public FieldCapabilities Float => BackingDictionary.TryGetValue("float", out var f) ? f : null;
		public FieldCapabilities FloatRange => BackingDictionary.TryGetValue("float_range", out var f) ? f : null;
		public FieldCapabilities GeoPoint => BackingDictionary.TryGetValue("geo_point", out var f) ? f : null;
		public FieldCapabilities GeoShape => BackingDictionary.TryGetValue("geo_shape", out var f) ? f : null;
		public FieldCapabilities Shape => BackingDictionary.TryGetValue("shape", out var f) ? f : null;
		public FieldCapabilities HalfFloat => BackingDictionary.TryGetValue("half_float", out var f) ? f : null;
		public FieldCapabilities Id => BackingDictionary.TryGetValue("_id", out var f) ? f : null;
		public FieldCapabilities Index => BackingDictionary.TryGetValue("_index", out var f) ? f : null;
		public FieldCapabilities Integer => BackingDictionary.TryGetValue("integer", out var f) ? f : null;
		public FieldCapabilities IntegerRange => BackingDictionary.TryGetValue("integer_range", out var f) ? f : null;
		public FieldCapabilities Ip => BackingDictionary.TryGetValue("ip", out var f) ? f : null;
		public FieldCapabilities Keyword => BackingDictionary.TryGetValue("keyword", out var f) ? f : null;
		public FieldCapabilities Long => BackingDictionary.TryGetValue("long", out var f) ? f : null;
		public FieldCapabilities LongRange => BackingDictionary.TryGetValue("long_range", out var f) ? f : null;
		public FieldCapabilities Murmur3 => BackingDictionary.TryGetValue("murmur3", out var f) ? f : null;
		public FieldCapabilities Parent => BackingDictionary.TryGetValue("_parent", out var f) ? f : null;
		public FieldCapabilities Percolator => BackingDictionary.TryGetValue("percolator", out var f) ? f : null;
		public FieldCapabilities Routing => BackingDictionary.TryGetValue("_routing", out var f) ? f : null;
		public FieldCapabilities ScaledFloat => BackingDictionary.TryGetValue("scaled_float", out var f) ? f : null;
		public FieldCapabilities SearchAsYouType => BackingDictionary.TryGetValue("search_as_you_type", out var f) ? f : null;
		public FieldCapabilities Short => BackingDictionary.TryGetValue("short", out var f) ? f : null;
		public FieldCapabilities Source => BackingDictionary.TryGetValue("_source", out var f) ? f : null;
		public FieldCapabilities Text => BackingDictionary.TryGetValue("text", out var f) ? f : null;
		public FieldCapabilities TokenCount => BackingDictionary.TryGetValue("token_count", out var f) ? f : null;
		public FieldCapabilities Type => BackingDictionary.TryGetValue("_type", out var f) ? f : null;
		public FieldCapabilities Uid => BackingDictionary.TryGetValue("_uid", out var f) ? f : null;
		public FieldCapabilities ParentJoin => BackingDictionary.TryGetValue("_parent_join", out var f) ? f : null;
		public FieldCapabilities Version => BackingDictionary.TryGetValue("_version", out var f) ? f : null;
		public FieldCapabilities VersionField => BackingDictionary.TryGetValue("version", out var f) ? f : null;
	}

	public class FieldCapabilities
	{
		[DataMember(Name = "aggregatable")]
		public bool Aggregatable { get; internal set; }

		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		public Indices Indices { get; internal set; }

		[DataMember(Name = "non_aggregatable_indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		public Indices NonAggregatableIndices { get; internal set; }

		[DataMember(Name = "non_searchable_indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		public Indices NonSearchableIndices { get; internal set; }

		[DataMember(Name = "searchable")]
		public bool Searchable { get; internal set; }

		[DataMember(Name = "meta")]
		public IReadOnlyDictionary<string, string[]> Meta { get; internal set; } = EmptyReadOnly<string, string[]>.Dictionary;
	}
}
