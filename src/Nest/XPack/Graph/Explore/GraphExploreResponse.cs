using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGraphExploreResponse : IResponse
	{
		[JsonProperty("took")]
		long Took { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		[JsonProperty("vertices")]
		IReadOnlyCollection<GraphVertex> Vertices { get; }

		[JsonProperty("connections")]
		IReadOnlyCollection<GraphConnection> Connections { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<ShardFailure> Failures { get; }
	}

	public class GraphExploreResponse : ResponseBase, IGraphExploreResponse
	{
		public long Took { get; internal set; }

		public bool TimedOut { get; internal set; }
		public IReadOnlyCollection<GraphConnection> Connections { get; internal set; } = EmptyReadOnly<GraphConnection>.Collection;
		public IReadOnlyCollection<GraphVertex> Vertices { get; internal set; } = EmptyReadOnly<GraphVertex>.Collection;
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;
	}
}
