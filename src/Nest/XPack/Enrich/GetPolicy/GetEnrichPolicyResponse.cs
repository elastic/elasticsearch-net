using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetEnrichPolicyResponse : ResponseBase
	{
		[DataMember(Name = "policies")]
		public IReadOnlyCollection<NamedPolicyMetadata> Policies { get; internal set; } = EmptyReadOnly<NamedPolicyMetadata>.Collection;
	}
}
