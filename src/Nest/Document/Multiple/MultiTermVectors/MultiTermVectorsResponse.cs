using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class MultiTermVectorsResponse : ResponseBase
	{
		[DataMember(Name ="docs")]
		public IReadOnlyCollection<ITermVectors> Documents { get; internal set; } = EmptyReadOnly<ITermVectors>.Collection;
	}
}
