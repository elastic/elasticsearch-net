using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatRecoveryRecord : ICatRecord
	{
		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public string Shard  { get; set; }

		[JsonProperty("time")]
		public string Time { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("stage")]
		public string Stage { get; set; }

		[JsonProperty("source_host")]
		public string SourceHost { get; set; }

		[JsonProperty("source_node")]
		public string SourceNode { get; set; }

		[JsonProperty("target_host")]
		public string TargetHost { get; set; }

		[JsonProperty("target_node")]
		public string TargetNode { get; set; }

		[JsonProperty("repository")]
		public string Repository { get; set; }

		[JsonProperty("snapshot")]
		public string Snapshot { get; set; }

		[JsonProperty("files")]
		public string Files { get; set; }

		[JsonProperty("files_recovered")]
		public string FilesRecovered { get; set; }

		[JsonProperty("files_percent")]
		public string FilesPercent { get; set; }

		[JsonProperty("files_total")]
		public string FilesTotal { get; set; }

		[JsonProperty("bytes")]
		public string Bytes { get; set; }

		[JsonProperty("bytes_recovered")]
		public string BytesRecovered { get; set; }

		[JsonProperty("bytes_percent")]
		public string BytesPercent { get; set; }

		[JsonProperty("bytes_total")]
		public string BytesTotal { get; set; }

		[JsonProperty("translog_ops")]
		public long? TranslogOps { get; set; }

		[JsonProperty("translog_ops_percent")]
		public string TranslogOpsPercent { get; set; }

		[JsonProperty("translog_ops_recovered")]
		public long? TranslogOpsRecovered { get; set; }
	}
}
