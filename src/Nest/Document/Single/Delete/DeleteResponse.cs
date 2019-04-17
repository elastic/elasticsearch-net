using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class DeleteResponse : ResponseBase
	{
		/// <summary> The ID of the deleted document. </summary>
		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="_primary_term")]
		public long PrimaryTerm { get; internal set; }

		/// <summary> The operation that was performed on the document. </summary>
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
