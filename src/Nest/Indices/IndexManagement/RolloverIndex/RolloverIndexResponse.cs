using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRolloverIndexResponse : IAcknowledgedResponse
	{
		[DataMember(Name = "conditions")]
		IReadOnlyDictionary<string, bool> Conditions { get; }

		[DataMember(Name = "dry_run")]
		bool DryRun { get; }

		[DataMember(Name = "new_index")]
		string NewIndex { get; }

		[DataMember(Name = "old_index")]
		string OldIndex { get; }

		[DataMember(Name = "rolled_over")]
		bool RolledOver { get; }

		[DataMember(Name = "shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class RolloverIndexResponse : AcknowledgedResponseBase, IRolloverIndexResponse
	{
		public IReadOnlyDictionary<string, bool> Conditions { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;
		public bool DryRun { get; internal set; }

		public string NewIndex { get; internal set; }

		public string OldIndex { get; internal set; }

		public bool RolledOver { get; internal set; }

		public bool ShardsAcknowledged { get; internal set; }
	}
}
