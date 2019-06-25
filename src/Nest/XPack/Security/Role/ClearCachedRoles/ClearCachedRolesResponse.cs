using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class ClearCachedRolesResponse : ResponseBase
	{
		[DataMember(Name = "cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name = "nodes")]
		public IReadOnlyDictionary<string, SecurityNode> Nodes { get; internal set; } = EmptyReadOnly<string, SecurityNode>.Dictionary;
	}
}
