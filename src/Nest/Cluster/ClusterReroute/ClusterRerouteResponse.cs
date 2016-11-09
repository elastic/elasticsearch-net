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
		//TODO why is this not on the interface?
		public int Version { get; internal set; }
		public ClusterRerouteState State { get; internal set; }
		public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; } = EmptyReadOnly<ClusterRerouteExplanation>.Collection;
	}
}
