using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RollupJobStatus
	{
		[DataMember(Name ="current_position")]
		public IReadOnlyDictionary<string, object> CurrentPosition { get; internal set; } =
			EmptyReadOnly<string, object>.Dictionary;

		[DataMember(Name ="job_state")]
		public IndexingJobState JobState { get; internal set; }

		[DataMember(Name ="upgraded_doc_id")]
		public bool UpgradedDocId { get; internal set; }
	}
}
