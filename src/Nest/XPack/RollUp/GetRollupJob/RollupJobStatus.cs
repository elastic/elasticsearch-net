using Newtonsoft.Json;

namespace Nest
{
	public class RollupJobStatus
	{
		[JsonProperty("job_state")]
		public IndexingJobState JobState { get; internal set; }

		[JsonProperty("upgraded_doc_id")]
		public bool UpgradedDocId { get; internal set; }
	}
}
