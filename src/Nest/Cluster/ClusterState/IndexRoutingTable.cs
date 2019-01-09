using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public class IndexRoutingTable
	{
		[DataMember(Name = "shards")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, List<RoutingShard>>))]
		public IReadOnlyDictionary<string, List<RoutingShard>> Shards { get; internal set; }
	}
}
