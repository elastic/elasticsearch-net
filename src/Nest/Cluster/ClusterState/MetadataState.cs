using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class MetadataState
	{
		[DataMember(Name ="cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[DataMember(Name ="indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, MetadataIndexState>))]
		public IReadOnlyDictionary<string, MetadataIndexState> Indices { get; internal set; }

		[DataMember(Name ="templates")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, TemplateMapping>))]
		public IReadOnlyDictionary<string, TemplateMapping> Templates { get; internal set; }
	}
}
