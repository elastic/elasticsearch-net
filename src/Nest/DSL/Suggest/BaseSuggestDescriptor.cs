using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISuggester
	{
		string _Text { get; set; }

		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "shard_size")]
		int? ShardSize { get; set; }
	}

	public abstract class Suggester : ISuggester
	{
		public string _Text { get; set; }
		public PropertyPathMarker Field { get; set; }
		public string Analyzer { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class BaseSuggestDescriptor<T> : ISuggester where T : class
	{
		string ISuggester._Text { get; set; }

		PropertyPathMarker ISuggester.Field { get; set; }

		string ISuggester.Analyzer { get; set; }

		int? ISuggester.Size { get; set; }

		int? ISuggester.ShardSize { get; set; }
	}
}
