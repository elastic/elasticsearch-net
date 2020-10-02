// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GetModelSnapshotsResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="model_snapshots")]
		public IReadOnlyCollection<ModelSnapshot> ModelSnapshots { get; internal set; } = EmptyReadOnly<ModelSnapshot>.Collection;
	}
}
