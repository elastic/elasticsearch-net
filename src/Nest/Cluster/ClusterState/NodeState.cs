using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public class NodeState
	{
		[DataMember(Name ="attributes")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, string>))]
		public Dictionary<string, string> Attributes { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="transport_address")]
		public string TransportAddress { get; internal set; }
	}
}
