using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatRecoveryRecord : ICatRecord
	{
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

		[JsonProperty("target_host")]
		public string TargetHost { get; set; }

		[JsonProperty("repository")]
		public string Repository { get; set; }

		[JsonProperty("snapshot")]
		public string Snapshot { get; set; }

		[JsonProperty("files")]
		public string Files { get; set; }

		[JsonProperty("files_percent")]
		public string FilesPercent { get; set; }

		[JsonProperty("bytes")]
		public string Bytes { get; set; }

		[JsonProperty("bytes_percent")]
		public string BytesPercent { get; set; }

		[JsonProperty("translog")]
		public long? Translog { get; set; }

		[JsonProperty("translog_percent")]
		public string TranslogPercent { get; set; }

		[JsonProperty("total_translog")]
		public long? TotalTranslog { get; set; }


	}
}