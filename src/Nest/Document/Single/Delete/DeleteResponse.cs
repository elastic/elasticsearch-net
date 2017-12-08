using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteResponse : IResponse
	{
		/// <summary>
		/// The ID of the deleted document.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// The index of the deleted document.
		/// </summary>
		string Index { get; }

		/// <summary>
		/// The type of the deleted document.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// The version of the deleted document.
		/// </summary>
		long Version { get; }

		/// <summary>
		/// The operation that was performed on the document.
		/// </summary>
		Result Result { get; }

		ShardStatistics Shards { get; }
		long SequenceNumber { get; }
		long PrimaryTerm { get; }
	}


	[JsonObject]
	public class DeleteResponse : ResponseBase, IDeleteResponse
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }

		[JsonProperty("result")]
		public Result Result { get; internal set; }

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }

		[JsonProperty("_seq_no")]
		public long SequenceNumber { get; internal set; }

		[JsonProperty("_primary_term")]
		public long PrimaryTerm { get; internal set; }
	}
}
