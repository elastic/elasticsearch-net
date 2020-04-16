using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class ReloadSearchAnalyzersResponse : ResponseBase
	{
		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }

		[DataMember(Name ="reload_details")]
		public IReadOnlyCollection<ReloadDetails> ReloadDetails { get; internal set; }  = EmptyReadOnly<ReloadDetails>.Collection;
	}
}
