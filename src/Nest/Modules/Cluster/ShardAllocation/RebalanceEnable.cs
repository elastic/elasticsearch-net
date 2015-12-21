using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RebalanceEnable
	{
		/// <summary>
		/// (default) Allows shard balancing for all kinds of shards.
		/// </summary>
		[EnumMember(Value = "all")]
		All,

		/// <summary>
		/// Allows shard balancing only for primary shards.
		/// </summary>
		[EnumMember(Value = "primaries")]
		Primaries,

		/// <summary>
		/// Allows shard balancing only for replica shards.
		/// </summary>
		[EnumMember(Value = "replicas")]
		Replicas,

		/// <summary>
		/// No shard balancing of any kind are allowed for any indices.
		/// </summary>
		[EnumMember(Value = "none")]
		None,

	}
}