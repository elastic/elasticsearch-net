using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IClusterRerouteResponse : IResponse
	{
		[JsonProperty("state")]
		ClusterRerouteState State { get; set; }

		[JsonProperty("explanations")]
		IEnumerable<ClusterRerouteExplanation> Explanations { get; set; }
	}

	public class ClusterRerouteResponse : ResponseBase, IClusterRerouteResponse
	{
		public int Version { get; set; }
		public ClusterRerouteState State { get; set; }
		public IEnumerable<ClusterRerouteExplanation> Explanations { get; set; }
	}
}
