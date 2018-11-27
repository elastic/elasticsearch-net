using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteExplanation
	{
		[DataMember(Name ="command")]
		public string Command { get; set; }

		[DataMember(Name ="decisions")]
		public IEnumerable<ClusterRerouteDecision> Decisions { get; set; }

		[DataMember(Name ="parameters")]
		public ClusterRerouteParameters Parameters { get; set; }
	}
}
