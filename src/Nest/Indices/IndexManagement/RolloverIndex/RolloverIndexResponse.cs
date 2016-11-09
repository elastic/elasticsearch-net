using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IRolloverIndexResponse : IAcknowledgedResponse
	{
		[JsonProperty("old_index")]
		string OldIndex { get; }

		[JsonProperty("new_index")]
		string NewIndex { get; }

		[JsonProperty("rolled_over")]
		bool RolledOver { get; }

		[JsonProperty("dry_run")]
		bool DryRun { get; }

		[JsonProperty("conditions")]
		IReadOnlyDictionary<string, bool> Conditions { get; }

		[JsonProperty("shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class RolloverIndexResponse : AcknowledgedResponseBase, IRolloverIndexResponse
	{
		public bool DryRun { get; internal set; }

		public string NewIndex { get; internal set; }

		public string OldIndex { get; internal set; }

		public bool RolledOver { get; internal set; }

		public IReadOnlyDictionary<string, bool> Conditions { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;

		public bool ShardsAcknowledged { get; internal set; }
	}
}
