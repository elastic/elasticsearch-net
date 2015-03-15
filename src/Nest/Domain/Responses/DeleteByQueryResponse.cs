using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IDeleteByQueryResponse : IResponse
	{
		/// <summary>
		/// The delete by query details for each affected index.
		/// </summary>
		IDictionary<string, DeleteByQueryIndices> Indices { get; set; }
	}


	[JsonObject]
	public class DeleteByQueryResponse : BaseResponse, IDeleteByQueryResponse
	{
		[JsonProperty("_indices")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<string, DeleteByQueryIndices> Indices { get; set; }
	}

	[JsonObject]
	public class DeleteByQueryIndices
	{
		[JsonProperty("_shards")]
		public ShardsMetaData Shards { get; set; }
	}
}
