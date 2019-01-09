using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public class MetadataState
	{
		[DataMember(Name = "cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, MetadataIndexState>))]
		public IReadOnlyDictionary<string, MetadataIndexState> Indices { get; internal set; }

		[DataMember(Name = "templates")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, TemplateMapping>))]
		public IReadOnlyDictionary<string, TemplateMapping> Templates { get; internal set; }
	}
}
