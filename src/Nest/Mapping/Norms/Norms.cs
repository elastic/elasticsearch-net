using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Norms>))]
	public interface INorms
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("loading")]
		[JsonConverter(typeof(StringEnumConverter))]
		NormsLoading? Loading { get; set; }
	}

	public class Norms : INorms
	{
		public bool? Enabled { get; set; }
		public NormsLoading? Loading { get; set; }
	}

	public class NormsDescriptor : DescriptorBase<NormsDescriptor, INorms>, INorms
	{
		bool? INorms.Enabled { get; set; }
		NormsLoading? INorms.Loading { get; set; }

		public NormsDescriptor Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);
		public NormsDescriptor Loading(NormsLoading loading) => Assign(a => a.Loading = loading);
	}
}