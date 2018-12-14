using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class BulkIndexByScrollFailure
	{
		[DataMember(Name ="cause")]
		public BulkIndexFailureCause Cause { get; set; }

		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		[DataMember(Name ="index")]
		public string Index { get; set; }

		[DataMember(Name ="status")]
		public int Status { get; set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}

	[DataContract]
	[JsonFormatter(typeof(ErrorCauseFormatter<BulkIndexFailureCause>))]
	public class BulkIndexFailureCause : Error
	{
		public string Index => Metadata?.Index;
		public string IndexUniqueId => Metadata?.IndexUUID;
		public int? Shard => Metadata?.Shard;
	}
}
