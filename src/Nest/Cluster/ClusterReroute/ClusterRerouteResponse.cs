using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IClusterRerouteResponse : IResponse
	{
		[DataMember(Name ="explanations")]
		IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; }

		[DataMember(Name ="state")]
		ClusterRerouteState State { get; }
	}

	public class ClusterRerouteResponse : ResponseBase, IClusterRerouteResponse
	{
		public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; internal set; } =
			EmptyReadOnly<ClusterRerouteExplanation>.Collection;

		public ClusterRerouteState State { get; internal set; }
	}
}
