using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexRoutingTable
	{
		[DataMember(Name = "shards")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, List<RoutingShard>>))]
		public IReadOnlyDictionary<string, List<RoutingShard>> Shards { get; internal set; }
	}
}
