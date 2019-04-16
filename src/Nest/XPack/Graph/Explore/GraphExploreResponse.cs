using System.Collections.Generic;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGraphExploreResponse : IResponse
	{
		[DataMember(Name ="connections")]
		IReadOnlyCollection<GraphConnection> Connections { get; }

		[DataMember(Name ="failures")]
		IReadOnlyCollection<ShardFailure> Failures { get; }

		[DataMember(Name ="timed_out")]
		bool TimedOut { get; }

		[DataMember(Name ="took")]
		long Took { get; }

		[DataMember(Name ="vertices")]
		IReadOnlyCollection<GraphVertex> Vertices { get; }
	}

	public class GraphExploreResponse : ResponseBase, IGraphExploreResponse
	{
		public IReadOnlyCollection<GraphConnection> Connections { get; internal set; } = EmptyReadOnly<GraphConnection>.Collection;
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;

		public bool TimedOut { get; internal set; }
		public long Took { get; internal set; }
		public IReadOnlyCollection<GraphVertex> Vertices { get; internal set; } = EmptyReadOnly<GraphVertex>.Collection;
	}
}
