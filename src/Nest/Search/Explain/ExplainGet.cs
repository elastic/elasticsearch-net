using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class InstantGet<T> where T : class
	{
		[JsonProperty(PropertyName = "fields")]
		public FieldValues Fields { get; internal set; }

		[JsonProperty(PropertyName = "found")]
		public bool Found { get; internal set; }

		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }
	}
}
