// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class AddIndexBlockResponse : AcknowledgedResponseBase
	{
		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }

		[DataMember(Name = "indices")]
		public IReadOnlyCollection<BlockedIndex> Indices { get; internal set; } = EmptyReadOnly<BlockedIndex>.Collection;
	}

	public class BlockedIndex
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "blocked")]
		public bool Blocked { get; internal set; }
	}
}
