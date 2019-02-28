using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class NodeState
	{
		// TODO: IReadOnlyDictionary
		[DataMember(Name ="attributes")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, string>))]
		public Dictionary<string, string> Attributes { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="transport_address")]
		public string TransportAddress { get; internal set; }
	}
}
