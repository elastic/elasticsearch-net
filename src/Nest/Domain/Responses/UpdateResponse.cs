using System.Collections.Generic;
using Nest.Domain;
using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateResponse : IResponse
	{
		ShardsMetaData ShardsHit { get; }
		string Index { get; }
		string Type { get; }
		string Id { get; }
		string Version { get; }
		GetFromUpdate Get { get;  }

		T Source<T>() where T : class;
		FieldSelection<T> Fields<T>() where T : class;
	}

	[JsonObject]
	public class UpdateResponse : BaseResponse, IUpdateResponse
	{
		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; private set; }
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; private set; }
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; private set; }
		[JsonProperty(PropertyName = "_version")]
		public string Version { get; private set; }

		[JsonProperty(PropertyName = "get")]
		public GetFromUpdate Get { get; private set; }

		public T Source<T>() where T : class
		{
			if (this.Get == null) return null;
			return this.Get.Source.As<T>();
		}

		public FieldSelection<T> Fields<T>() where T : class
		{
			if (this.Get == null) return null;
			return new FieldSelection<T>(this.Settings, this.Get._fields);
		}
	}

	[JsonObject]
	public class GetFromUpdate
	{
		[JsonProperty(PropertyName = "found")]
		public bool Found { get; set; }

		[JsonProperty(PropertyName = "_source")]
		internal IDocument Source { get; set; }


		[JsonProperty(PropertyName = "fields")]
		internal IDictionary<string, object> _fields { get; set; }


	}
}
