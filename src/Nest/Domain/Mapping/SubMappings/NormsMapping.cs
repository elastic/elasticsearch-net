using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class NormsMapping 
	{
		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		[JsonProperty("loading")]
		[JsonConverter(typeof(StringEnumConverter))]
		public NormsLoading? Loading { get; set; }

	}
}