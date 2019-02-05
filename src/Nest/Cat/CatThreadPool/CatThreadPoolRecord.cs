using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class CatThreadPoolRecord : ICatRecord
	{
		[DataMember(Name ="active")]
		[JsonFormatter(typeof(StringIntFormatter))]
		public int Active { get; set; }

		[DataMember(Name ="completed")]
		[JsonFormatter(typeof(NullableStringLongFormatter))]
		public long? Completed { get; set; }

		[DataMember(Name ="ephemeral_node_id")]
		public string EphemeralNodeId { get; set; }

		[DataMember(Name ="host")]
		public string Host { get; set; }

		[DataMember(Name ="ip")]
		public string Ip { get; set; }

		[DataMember(Name ="keep_alive")]
		public Time KeepAlive { get; set; }

		[DataMember(Name ="largest")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Largest { get; set; }

		//TODO: This is now often reported back as null since 7.x (investigate)
		[DataMember(Name ="max")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Maximum { get; set; }

		//TODO: this appears to no longer be reported
		[DataMember(Name ="min")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Minimum { get; set; }

		[DataMember(Name ="name")]
		public string Name { get; set; }

		[DataMember(Name ="node_id")]
		public string NodeId { get; set; }

		[DataMember(Name ="node_name")]
		public string NodeName { get; set; }

		[DataMember(Name ="port")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Port { get; set; }

		[DataMember(Name ="pid")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? ProcessId { get; set; }

		[DataMember(Name ="queue")]
		[JsonFormatter(typeof(StringIntFormatter))]
		public int Queue { get; set; }

		[DataMember(Name ="queue_size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? QueueSize { get; set; }

		[DataMember(Name ="rejected")]
		[JsonFormatter(typeof(StringLongFormatter))]
		public long Rejected { get; set; }

		[DataMember(Name ="size")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		public int? Size { get; set; }

		[DataMember(Name ="type")]
		public string Type { get; set; }
	}
}
