using Newtonsoft.Json;

namespace Nest
{
	public class RollupJobStats
	{
		[JsonProperty("documents_processed")]
		public long DocumentsProcessed { get; internal set; }

		[JsonProperty("pages_processed")]
		public long PagesProcessed { get; internal set; }

		[JsonProperty("rollups_indexed")]
		public long RollupsIndexed { get; internal set; }

		[JsonProperty("trigger_count")]
		public long TriggerCount { get; internal set; }

		[JsonProperty("search_failures")]
		public long SearchFailures { get; internal set; }

		[JsonProperty("index_failures")]
		public long IndexFailures { get; internal set; }

		[JsonProperty("index_time_in_ms")]
		public long IndexTimeInMilliseconds { get; internal set; }

		[JsonProperty("index_total")]
		public long IndexTotal { get; internal set; }

		[JsonProperty("search_time_in_ms")]
		public long SearchTimeInMilliseconds { get; internal set; }

		[JsonProperty("search_total")]
		public long SearchTotal { get; internal set; }
	}
}
