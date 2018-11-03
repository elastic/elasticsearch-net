using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class RollupJobStatus
	{
		[JsonProperty("current_position")]
		public IReadOnlyDictionary<string, object> CurrentPosition { get; internal set; } =
			EmptyReadOnly<string, object>.Dictionary;

		[JsonProperty("job_state")]
		public IndexingJobState JobState { get; internal set; }

		[JsonProperty("upgraded_doc_id")]
		public bool UpgradedDocId { get; internal set; }
	}
}
