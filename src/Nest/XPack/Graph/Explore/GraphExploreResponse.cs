using System.Collections.Generic;
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
		IEnumerable<GraphVertex> Vertices { get; }

		[JsonProperty("connections")]
		IEnumerable<GraphConnection> Connections { get; }

		[JsonProperty("failures")]
		IEnumerable<ShardFailure> Failures { get; }
	}

	public class GraphExploreResponse : ResponseBase, IGraphExploreResponse
	{
		public long Took { get; internal set; }

		public bool TimedOut { get; internal set; }
		public IEnumerable<GraphConnection> Connections { get; internal set; }
		public IEnumerable<GraphVertex> Vertices { get; internal set; }
		public IEnumerable<ShardFailure> Failures { get; internal set; }
	}
}
