using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public class NodeState
	{
		[DataMember(Name ="attributes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, string>))]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="transport_address")]
		public string TransportAddress { get; internal set; }
	}
}
