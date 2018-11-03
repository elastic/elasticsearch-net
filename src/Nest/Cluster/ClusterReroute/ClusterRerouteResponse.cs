using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterRerouteResponse : IResponse
	{
		[JsonProperty("explanations")]
		IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; }

		[JsonProperty("state")]
		ClusterRerouteState State { get; }
	}

	public class ClusterRerouteResponse : ResponseBase, IClusterRerouteResponse
	{
		public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; internal set; } =
			EmptyReadOnly<ClusterRerouteExplanation>.Collection;

		public ClusterRerouteState State { get; internal set; }
	}
}
