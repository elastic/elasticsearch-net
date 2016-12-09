using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterRerouteResponse : IResponse
	{
		[JsonProperty("state")]
		ClusterRerouteState State { get; }

		[JsonProperty("explanations")]
		IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; }
	}

	public class ClusterRerouteResponse : ResponseBase, IClusterRerouteResponse
	{
		public ClusterRerouteState State { get; internal set; }
		public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; internal set; } = EmptyReadOnly<ClusterRerouteExplanation>.Collection;
	}
}
