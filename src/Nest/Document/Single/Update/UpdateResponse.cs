using System.Runtime.Serialization;

namespace Nest
{
	public interface IUpdateResponse<out TDocument> : IResponse where TDocument : class
	{
		IInlineGet<TDocument> Get { get; }
	}

	[DataContract]
	public class UpdateResponse<TDocument> : ResponseBase, IUpdateResponse<TDocument>
		where TDocument : class
	{
		[DataMember(Name ="get")]
		public IInlineGet<TDocument> Get { get; internal set; }

		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="result")]
		public Result Result { get; internal set; }

		[DataMember(Name ="_shards")]
		public ShardStatistics ShardsHit { get; internal set; }

		[DataMember(Name ="_type")]
		public string Type { get; internal set; }

		[DataMember(Name ="_version")]
		public long Version { get; internal set; }
	}
}
