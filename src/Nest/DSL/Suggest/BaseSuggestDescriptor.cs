using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class BaseSuggestDescriptor<T> : ISuggestDescriptor<T> where T : class
	{
		internal string _Text { get; set; }

		[JsonProperty(PropertyName = "field")]
		internal string _Field { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }

		[JsonProperty(PropertyName = "size")]
		internal int? _Size { get; set; }

		[JsonProperty(PropertyName = "shard_size")]
		internal int? _ShardSize { get; set; }
	}
}
