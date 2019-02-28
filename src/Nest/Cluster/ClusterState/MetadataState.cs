using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class MetadataState
	{
		[DataMember(Name = "cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, MetadataIndexState>))]
		public IReadOnlyDictionary<string, MetadataIndexState> Indices { get; internal set; }

		[DataMember(Name = "templates")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, TemplateMapping>))]
		public IReadOnlyDictionary<string, TemplateMapping> Templates { get; internal set; }
	}
}
