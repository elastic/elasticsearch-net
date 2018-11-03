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
	}
}
