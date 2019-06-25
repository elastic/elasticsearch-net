using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

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
