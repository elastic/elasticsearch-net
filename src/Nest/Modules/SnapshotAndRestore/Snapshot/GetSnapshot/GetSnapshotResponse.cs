using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class GetSnapshotResponse : ResponseBase
	{
		[DataMember(Name ="snapshots")]
		public IReadOnlyCollection<Snapshot> Snapshots { get; internal set; } = EmptyReadOnly<Snapshot>.Collection;
	}
}
