using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiGetHit<out T> where T : class
	{
		T Source { get; }

		string Index { get; }

		bool Found { get; }

		string Type { get; }

		long Version { get; }

		string Id { get; }
	}

	[JsonObject]
	public class MultiGetHit<T> : IMultiGetHit<T>
		where T : class
	{
		public FieldValues Fields { get; internal set; }

		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "found")]
		public bool Found { get; internal set; }

		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }

		[JsonProperty(PropertyName = "_version")]
		public long Version { get; internal set; }

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }
	}
}
