using System;
using Newtonsoft.Json;

namespace Nest
{
	public class DateRange
	{
		[JsonProperty("gte")]
		public DateTimeOffset? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		public DateTimeOffset? LessThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		public DateTimeOffset? GreaterThan { get; set; }

		[JsonProperty("lt")]
		public DateTimeOffset? LessThan { get; set; }
	}
	public class DoubleRange
	{
		[JsonProperty("gte")]
		public double? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		public double? LessThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		public double? GreaterThan { get; set; }

		[JsonProperty("lt")]
		public double? LessThan { get; set; }
	}
	public class FloatRange
	{
		[JsonProperty("gte")]
		public float? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		public float? LessThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		public float? GreaterThan { get; set; }

		[JsonProperty("lt")]
		public float? LessThan { get; set; }
	}
	public class IntegerRange
	{
		[JsonProperty("gte")]
		public int? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		public int? LessThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		public int? GreaterThan { get; set; }

		[JsonProperty("lt")]
		public int? LessThan { get; set; }
	}
	public class LongRange
	{
		[JsonProperty("gte")]
		public long? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		public long? LessThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		public long? GreaterThan { get; set; }

		[JsonProperty("lt")]
		public long? LessThan { get; set; }
	}
	public class IpAddressRange
	{
		[JsonProperty("gte")]
		public string GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		public string LessThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		public string GreaterThan { get; set; }

		[JsonProperty("lt")]
		public string LessThan { get; set; }
	}
}
