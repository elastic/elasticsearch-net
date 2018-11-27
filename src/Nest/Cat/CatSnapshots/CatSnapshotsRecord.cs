using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatSnapshotsRecord : ICatRecord
	{
		[DataMember(Name ="duration")]
		public Time Duration { get; set; }

		[DataMember(Name ="end_epoch")]
		public long EndEpoch { get; set; }

		[DataMember(Name ="end_time")]
		public string EndTime { get; set; }

		[DataMember(Name ="failed_shards")]
		public long FailedShards { get; set; }

		// duration indices successful_shards failed_shards total_shards
		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="indices")]
		public long Indices { get; set; }

		[DataMember(Name ="start_epoch")]
		public long StartEpoch { get; set; }

		[DataMember(Name ="start_time")]
		public string StartTime { get; set; }

		[DataMember(Name ="status")]
		public string Status { get; set; }

		[DataMember(Name ="succesful_shards")]
		public long SuccesfulShards { get; set; }

		[DataMember(Name ="total_shards")]
		public long TotalShards { get; set; }
	}
}
