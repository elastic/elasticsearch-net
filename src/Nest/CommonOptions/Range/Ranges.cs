using System;
using Newtonsoft.Json;

namespace Nest
{
	public class DateRange
	{
		[JsonProperty("gt")]
		public DateTimeOffset? GreaterThan { get; set; }

		[JsonProperty("gte")]
		public DateTimeOffset? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lt")]
		public DateTimeOffset? LessThan { get; set; }

		[JsonProperty("lte")]
		public DateTimeOffset? LessThanOrEqualTo { get; set; }
	}

	public class DoubleRange
	{
		[JsonProperty("gt")]
		public double? GreaterThan { get; set; }

		[JsonProperty("gte")]
		public double? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lt")]
		public double? LessThan { get; set; }

		[JsonProperty("lte")]
		public double? LessThanOrEqualTo { get; set; }
	}

	public class FloatRange
	{
		[JsonProperty("gt")]
		public float? GreaterThan { get; set; }

		[JsonProperty("gte")]
		public float? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lt")]
		public float? LessThan { get; set; }

		[JsonProperty("lte")]
		public float? LessThanOrEqualTo { get; set; }
	}

	public class IntegerRange
	{
		[JsonProperty("gt")]
		public int? GreaterThan { get; set; }

		[JsonProperty("gte")]
		public int? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lt")]
		public int? LessThan { get; set; }

		[JsonProperty("lte")]
		public int? LessThanOrEqualTo { get; set; }
	}

	public class LongRange
	{
		[JsonProperty("gt")]
		public long? GreaterThan { get; set; }

		[JsonProperty("gte")]
		public long? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lt")]
		public long? LessThan { get; set; }

		[JsonProperty("lte")]
		public long? LessThanOrEqualTo { get; set; }
	}

	public class IpAddressRange
	{
		[JsonProperty("gt")]
		public string GreaterThan { get; set; }

		[JsonProperty("gte")]
		public string GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lt")]
		public string LessThan { get; set; }

		[JsonProperty("lte")]
		public string LessThanOrEqualTo { get; set; }
	}
}
