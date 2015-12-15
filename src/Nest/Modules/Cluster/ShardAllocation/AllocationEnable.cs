using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AllocationEnable
	{
		/// <summary>
		///  (default) Allows shard allocation for all kinds of shards.
		/// </summary>
		[EnumMember(Value = "all")]
		All,

		/// <summary>
		/// Allows shard allocation only for primary shards.
		/// </summary>
		[EnumMember(Value = "primaries")]
		Primaries,

		/// <summary>
		/// Allows shard allocation only for primary shards for new indices.
		/// </summary>
		[EnumMember(Value = "new_primaries")]
		NewPrimaries,

		/// <summary>
		/// No shard allocations of any kind are allowed for any indices.
		/// </summary>
		[EnumMember(Value = "none")]
		None,

	}
}