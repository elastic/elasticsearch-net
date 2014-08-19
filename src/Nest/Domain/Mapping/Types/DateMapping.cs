using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DateMapping : MultiFieldMapping, IElasticType, IElasticCoreType
	{
		public DateMapping():base("date")
		{
		}

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("store")]
		public bool? Store { get; set; }

		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public NonStringIndexOption? Index { get; set; }

		[JsonProperty("format")]
		public string Format { get; set; }

		[JsonProperty("precision_step")]
		public int? PrecisionStep { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public DateTime? NullValue { get; set; }

		[JsonProperty("ignore_malformed")]
		public bool? IgnoreMalformed { get; set; }

	}
}