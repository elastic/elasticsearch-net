using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGraphExploreResponse : IResponse
	{
		[JsonProperty("connections")]
		IReadOnlyCollection<GraphConnection> Connections { get; }

		[JsonProperty("failures")]
		IReadOnlyCollection<ShardFailure> Failures { get; }

		[JsonProperty("timed_out")]
		bool TimedOut { get; }

		[JsonProperty("took")]
		long Took { get; }

		[JsonProperty("vertices")]
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
