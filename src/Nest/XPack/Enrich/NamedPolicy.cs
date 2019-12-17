using System.Runtime.Serialization;

namespace Nest
{
	public class NamedPolicy : EnrichPolicy
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
	}

	public class NamedPolicyMetadata
	{
		[DataMember(Name = "config")]
		public NamedPolicyConfig Config { get; internal set; }
	}

	public class NamedPolicyConfig
	{
		[DataMember(Name = "geo_match")]
		public NamedPolicy GeoMatch { get; internal set; }

		[DataMember(Name = "match")]
		public NamedPolicy Match { get; internal set; }
	}
}
