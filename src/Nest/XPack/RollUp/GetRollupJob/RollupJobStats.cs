using System.Runtime.Serialization;

namespace Nest
{
	public class RollupJobStats
	{
		[DataMember(Name ="documents_processed")]
		public long DocumentsProcessed { get; internal set; }

		[DataMember(Name ="pages_processed")]
		public long PagesProcessed { get; internal set; }

		[DataMember(Name ="rollups_indexed")]
		public long RollupsIndexed { get; internal set; }

		[DataMember(Name ="trigger_count")]
		public long TriggerCount { get; internal set; }
	}
}
