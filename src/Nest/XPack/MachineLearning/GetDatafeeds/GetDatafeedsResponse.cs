using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetDatafeedsResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="datafeeds")]
		public IReadOnlyCollection<DatafeedConfig> Datafeeds { get; internal set; } = EmptyReadOnly<DatafeedConfig>.Collection;
	}
}
