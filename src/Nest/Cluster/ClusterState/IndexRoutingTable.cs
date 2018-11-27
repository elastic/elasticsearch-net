using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class IndexRoutingTable
	{
		[DataMember(Name ="shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, List<RoutingShard>>))]
		public IReadOnlyDictionary<string, List<RoutingShard>> Shards { get; internal set; }
	}
}
