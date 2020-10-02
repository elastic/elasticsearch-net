// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RolloverIndexResponse : AcknowledgedResponseBase
	{
		[DataMember(Name = "conditions")]
		public IReadOnlyDictionary<string, bool> Conditions { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;

		[DataMember(Name = "dry_run")]
		public bool DryRun { get; internal set; }

		[DataMember(Name = "new_index")]
		public string NewIndex { get; internal set; }

		[DataMember(Name = "old_index")]
		public string OldIndex { get; internal set; }

		[DataMember(Name = "rolled_over")]
		public bool RolledOver { get; internal set; }

		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }
	}
}
