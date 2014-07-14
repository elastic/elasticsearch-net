using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IStatusResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		Dictionary<string, IndexStatus> Indices { get; }
	}

	[JsonObject]
	public class StatusResponse : BaseResponse, IStatusResponse
	{


		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, IndexStatus> Indices { get; internal set; }

	}
}
