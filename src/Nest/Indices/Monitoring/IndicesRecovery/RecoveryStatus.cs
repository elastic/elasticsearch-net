using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class RecoveryStatus
	{
		[DataMember(Name ="shards")]
		public IReadOnlyCollection<ShardRecovery> Shards { get; internal set; } =
			EmptyReadOnly<ShardRecovery>.Collection;
	}
}
