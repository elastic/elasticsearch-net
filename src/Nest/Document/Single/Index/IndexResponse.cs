using System.Runtime.Serialization;

namespace Nest
{
	public interface IIndexResponse : IResponse
	{
		string Id { get; }
		string Index { get; }
		long PrimaryTerm { get; }
		Result Result { get; }
		long SequenceNumber { get; }
		ShardStatistics Shards { get; }
		string Type { get; }
		long Version { get; }
	}

	[DataContract]
	public class IndexResponse : ResponseBase, IIndexResponse
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
