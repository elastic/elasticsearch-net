using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteResponse : ResponseBase
	{
		[DataMember(Name ="explanations")]
		public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; internal set; } =
			EmptyReadOnly<ClusterRerouteExplanation>.Collection;

		[DataMember(Name ="state")]
		public DynamicBody State { get; internal set; }
	}
}
