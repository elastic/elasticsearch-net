using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class RoutingTableState
	{
		[DataMember(Name ="indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndexRoutingTable>))]
		public IReadOnlyDictionary<string, IndexRoutingTable> Indices { get; internal set; } = EmptyReadOnly<string, IndexRoutingTable>.Dictionary;
	}
}
