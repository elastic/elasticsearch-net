using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetJobStatsResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="jobs")]
		public IReadOnlyCollection<JobStats> Jobs { get; internal set; } = EmptyReadOnly<JobStats>.Collection;
	}
}
