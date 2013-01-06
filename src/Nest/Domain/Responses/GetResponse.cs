using Nest.Domain;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetResponse<T> : IResponse where T : class
	{
		bool Exists { get; }
		string Index { get; }
		string Type { get; }
		string Id { get; }
		string Version { get; }
		T Source { get; }
		FieldSelection<T> Fields { get; }
	}

	[JsonObject]
	public class GetResponse<T> : BaseResponse, IGetResponse<T> where T : class
	{
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; private set; }
		
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; private set; }
		
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; private set; }
		
		[JsonProperty(PropertyName = "_version")]
		public string Version { get; private set; }

		[JsonProperty(PropertyName = "_exists")]
		public bool Exists { get; private set; }

		[JsonProperty(PropertyName = "_source")]
		public T Source { get; private set; }

		[JsonProperty(PropertyName = "fields")]
		public FieldSelection<T> Fields { get; internal set; }
	}
}
