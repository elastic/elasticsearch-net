using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class RoutingTableState
	{
		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, IndexRoutingTable>))]
		public IReadOnlyDictionary<string, IndexRoutingTable> Indices { get; internal set; } = EmptyReadOnly<string, IndexRoutingTable>.Dictionary;
	}
}
