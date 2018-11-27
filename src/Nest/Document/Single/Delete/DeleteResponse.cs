using System.Runtime.Serialization;

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

		long PrimaryTerm { get; }

		/// <summary>
		/// The operation that was performed on the document.
		/// </summary>
		Result Result { get; }

		long SequenceNumber { get; }

		ShardStatistics Shards { get; }

		/// <summary>
		/// The type of the deleted document.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// The version of the deleted document.
		/// </summary>
		long Version { get; }
	}


	[DataContract]
	public class DeleteResponse : ResponseBase, IDeleteResponse
	{
		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="_primary_term")]
		public long PrimaryTerm { get; internal set; }

		[DataMember(Name ="result")]
		public Result Result { get; internal set; }

		[DataMember(Name ="_seq_no")]
		public long SequenceNumber { get; internal set; }

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }

		[DataMember(Name ="_type")]
		public string Type { get; internal set; }

		[DataMember(Name ="_version")]
		public long Version { get; internal set; }
	}
}
