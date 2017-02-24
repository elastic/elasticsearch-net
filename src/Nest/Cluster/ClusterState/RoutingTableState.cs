using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class RoutingTableState
	{
		[JsonProperty("indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndexRoutingTable>))]
		public IReadOnlyDictionary<string, IndexRoutingTable> Indices { get; internal set; } = EmptyReadOnly<string, IndexRoutingTable>.Dictionary;
	}
}
