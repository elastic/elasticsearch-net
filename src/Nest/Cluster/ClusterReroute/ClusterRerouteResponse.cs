using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteResponse : ResponseBase
	{
		[DataMember(Name ="explanations")]
		public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; internal set; } =
			EmptyReadOnly<ClusterRerouteExplanation>.Collection;

		[DataMember(Name ="state")]
		public ClusterRerouteState State { get; internal set; }
	}
}
