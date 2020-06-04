// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Define the type of field content.
	/// </summary>
	[StringEnum]
	public enum FieldType
	{
		/// <summary>
		/// Default. Will be defined by the type of property return.
		/// </summary>
		[EnumMember(Value = "none")]
		None,

		[EnumMember(Value = "geo_point")]
		GeoPoint,

		[EnumMember(Value = "geo_shape")]
		GeoShape,

		/// <summary>
		/// An ip mapping type allows to store ipv4 addresses in a numeric form allowing to easily sort, and range query it (using ip values).
		/// </summary>
		[EnumMember(Value = "ip")]
		Ip,

		/// <summary>
		/// The binary type is a base64 representation of binary data that can be stored in the index.
		/// </summary>
		[EnumMember(Value = "binary")]
		Binary,

		[EnumMember(Value = "keyword")]
		Keyword,

		[EnumMember(Value = "text")]
		Text,

		/// <summary>
		/// A text-like field that is optimized to provide out-of-the-box support for queries that serve an as-you-type completion use case.
		/// </summary>
		[EnumMember(Value = "search_as_you_type")]
		SearchAsYouType,

		[EnumMember(Value = "date")]
		Date,

		[EnumMember(Value = "date_nanos")]
		DateNanos,

		[EnumMember(Value = "boolean")]
		Boolean,

		[EnumMember(Value = "completion")]
		Completion,

		[EnumMember(Value = "nested")]
		Nested,

		/// <summary>
		/// object type, no need to set this manually if its not a value type this will be set.
		/// Only set this if you need to force a value type to be mapped to an Elasticsearch object type.
		/// </summary>
		[EnumMember(Value = "object")]
		Object,

		/// <summary>
		/// Murmur hash type, for use with the cardinality aggregation.
		/// </summary>
		[EnumMember(Value = "murmur3")]
		Murmur3Hash,

		[EnumMember(Value = "token_count")]
		TokenCount,

		[EnumMember(Value = "percolator")]
		Percolator,

		[EnumMember(Value = "integer")]
		Integer,

		[EnumMember(Value = "long")]
		Long,

		[EnumMember(Value = "short")]
		Short,

		[EnumMember(Value = "byte")]
		Byte,

		[EnumMember(Value = "float")]
		Float,

		[EnumMember(Value = "half_float")]
		HalfFloat,

		[EnumMember(Value = "scaled_float")]
		ScaledFloat,

		[EnumMember(Value = "double")]
		Double,

		[EnumMember(Value = "integer_range")]
		IntegerRange,

		[EnumMember(Value = "float_range")]
		FloatRange,

		[EnumMember(Value = "long_range")]
		LongRange,

		[EnumMember(Value = "double_range")]
		DoubleRange,

		[EnumMember(Value = "date_range")]
		DateRange,

		[EnumMember(Value = "ip_range")]
		IpRange,

		[EnumMember(Value = "alias")]
		Alias,

		[EnumMember(Value = "join")]
		Join,

		[EnumMember(Value = "rank_feature")]
		RankFeature,

		[EnumMember(Value = "rank_features")]
		RankFeatures,

		[EnumMember(Value = "flattened")]
		Flattened,

		[EnumMember(Value = "shape")]
		Shape,

		[EnumMember(Value = "histogram")]
		Histogram,

		[EnumMember(Value = "constant_keyword")]
		ConstantKeyword,

		[EnumMember(Value = "wildcard")]
		Wildcard,

		[EnumMember(Value = "point")]
		Point,
	}
}
