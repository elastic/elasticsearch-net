using System.Runtime.Serialization;

namespace Nest
{
	public interface IUpdateResponse<TDocument> : IResponse
		where TDocument : class
	{
		[DataMember(Name ="get")]
		InstantGet<TDocument> Get { get; }

		[DataMember(Name ="_id")]
		string Id { get; }

		[DataMember(Name ="_index")]
		string Index { get; }

		[DataMember(Name ="result")]
		Result Result { get; }

		[DataMember(Name ="_shards")]
		ShardStatistics ShardsHit { get; }

		[DataMember(Name ="_type")]
		string Type { get; }

		[DataMember(Name ="_version")]
		long Version { get; }
	}

	[DataContract]
	public class UpdateResponse<TDocument> : ResponseBase, IUpdateResponse<TDocument>
		where TDocument : class
	{
		public InstantGet<TDocument> Get { get; internal set; }
		public string Id { get; internal set; }
		public string Index { get; internal set; }
		public Result Result { get; internal set; }
		public ShardStatistics ShardsHit { get; internal set; }
		public string Type { get; internal set; }
		public long Version { get; internal set; }
	}
}
