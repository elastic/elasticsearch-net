using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	// TODO: For backwards compatibility, this class acts as the response object
	// for both delete and delete by query requests, even though their actual responses
	// are completely different.  For 2.0, we should move the delete by query specific 
	// properties to a separate object (DeleteByQueryResponse).
	// See: https://github.com/elasticsearch/elasticsearch-net/issues/1146
	public interface IDeleteResponse : IResponse
	{
		/// <summary>
		/// The ID of the deleted document.
		/// NOTE: This property only applies to delete requests and will be 
		/// null for delete by query.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// The index of the deleted document.
		/// NOTE: This property only applies to delete requests and will be 
		/// null for delete by query.
		/// </summary>
		string Index { get; }

		/// <summary>
		/// The type of the deleted document.
		/// NOTE: This property only applies to delete requests and will be 
		/// null for delete by query.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// The version of the deleted document.
		/// NOTE: This property only applies to delete requests and will be 
		/// null for delete by query.
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Whether or not the document was found and deleted from the index.
		/// NOTE: This property only applies to delete requests and will be 
		/// false for delete by query.
		/// </summary>
		bool Found { get; }

		#region DeleteByQuery properties
		
		/// <summary>
		/// The delete by query details for each affected index.
		/// NOTE: This property only applies to delete by query requests and
		/// will be null for delete requests.
		/// </summary>
		IDictionary<string, DeleteByQueryIndices> Indices { get; set; }

		#endregion
	}


	[JsonObject]
	public class DeleteResponse : BaseResponse, IDeleteResponse
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }
		
		[JsonProperty("_id")]
		public string Id { get; internal set; }
		
		[JsonProperty("_version")]
		public string Version { get; internal set; }
		
		[JsonProperty("found")]
		public bool Found { get; internal set; }
		
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
