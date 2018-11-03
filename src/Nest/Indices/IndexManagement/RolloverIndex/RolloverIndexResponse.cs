using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IRolloverIndexResponse : IAcknowledgedResponse
	{
		[JsonProperty("conditions")]
		IReadOnlyDictionary<string, bool> Conditions { get; }

		[JsonProperty("dry_run")]
		bool DryRun { get; }

		[JsonProperty("new_index")]
		string NewIndex { get; }

		[JsonProperty("old_index")]
		string OldIndex { get; }

		[JsonProperty("rolled_over")]
		bool RolledOver { get; }

		[JsonProperty("shards_acknowledged")]
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
