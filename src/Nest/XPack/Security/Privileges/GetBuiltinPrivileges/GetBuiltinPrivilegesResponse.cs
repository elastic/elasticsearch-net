using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class GetBuiltinPrivilegesResponse : ResponseBase
	{
		[DataMember(Name = "cluster")]
		public IReadOnlyCollection<string> Cluster { get; internal set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name = "index")]
		public IReadOnlyCollection<string> Index { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
