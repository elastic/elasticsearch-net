// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class SnapshotRestore
	{
		[DataMember(Name ="indices")]
		public IReadOnlyCollection<IndexName> Indices { get; internal set; } =
			EmptyReadOnly<IndexName>.Collection;

		[DataMember(Name ="snapshot")]
		public string Name { get; internal set; }

		[DataMember(Name ="shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
