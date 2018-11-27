using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	public class ShardStatistics
	{
		[DataMember(Name ="failed")]
		public int Failed { get; internal set; }

		[DataMember(Name ="failures")]
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;

		[DataMember(Name ="successful")]
		public int Successful { get; internal set; }

		[DataMember(Name ="total")]
		public int Total { get; internal set; }
	}
}
