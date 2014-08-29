using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IClusterRerouteResponse : IResponse
	{
		[JsonProperty("state")]
		ClusterRerouteState State { get; set; }

		[JsonProperty("explanations")]
		IEnumerable<ClusterRerouteExplanation> Explanations { get; set; }
	}

	public class ClusterRerouteResponse : BaseResponse, IClusterRerouteResponse
	{
		public int Version { get; set; }
		public ClusterRerouteState State { get; set; }
		public IEnumerable<ClusterRerouteExplanation> Explanations { get; set; }
	}
}
