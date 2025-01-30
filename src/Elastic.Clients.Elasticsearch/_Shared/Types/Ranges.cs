// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public class DateRange
{
	[JsonPropertyName("gt")]
	public DateTimeOffset? GreaterThan { get; set; }

	[JsonPropertyName("gte")]
	public DateTimeOffset? GreaterThanOrEqualTo { get; set; }

	[JsonPropertyName("lt")]
	public DateTimeOffset? LessThan { get; set; }

	[JsonPropertyName("lte")]
	public DateTimeOffset? LessThanOrEqualTo { get; set; }
}

public class DoubleRange
{
	[JsonPropertyName("gt")]
	public double? GreaterThan { get; set; }

	[JsonPropertyName("gte")]
	public double? GreaterThanOrEqualTo { get; set; }

	[JsonPropertyName("lt")]
	public double? LessThan { get; set; }

	[JsonPropertyName("lte")]
	public double? LessThanOrEqualTo { get; set; }
}

public class FloatRange
{
	[JsonPropertyName("gt")]
	public float? GreaterThan { get; set; }

	[JsonPropertyName("gte")]
	public float? GreaterThanOrEqualTo { get; set; }

	[JsonPropertyName("lt")]
	public float? LessThan { get; set; }

	[JsonPropertyName("lte")]
	public float? LessThanOrEqualTo { get; set; }
}

public class IntegerRange
{
	[JsonPropertyName("gt")]
	public int? GreaterThan { get; set; }

	[JsonPropertyName("gte")]
	public int? GreaterThanOrEqualTo { get; set; }

	[JsonPropertyName("lt")]
	public int? LessThan { get; set; }

	[JsonPropertyName("lte")]
	public int? LessThanOrEqualTo { get; set; }
}

public class LongRange
{
	[JsonPropertyName("gt")]
	public long? GreaterThan { get; set; }

	[JsonPropertyName("gte")]
	public long? GreaterThanOrEqualTo { get; set; }

	[JsonPropertyName("lt")]
	public long? LessThan { get; set; }

	[JsonPropertyName("lte")]
	public long? LessThanOrEqualTo { get; set; }
}

public class IpAddressRange
{
	[JsonPropertyName("gt")]
	public string GreaterThan { get; set; }

	[JsonPropertyName("gte")]
	public string GreaterThanOrEqualTo { get; set; }

	[JsonPropertyName("lt")]
	public string LessThan { get; set; }

	[JsonPropertyName("lte")]
	public string LessThanOrEqualTo { get; set; }
}
