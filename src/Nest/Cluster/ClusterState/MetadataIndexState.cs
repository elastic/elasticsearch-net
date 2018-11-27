using System.Collections.Generic;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	public class MetadataIndexState
	{
		[DataMember(Name ="aliases")]
		public IEnumerable<string> Aliases { get; internal set; }

		[DataMember(Name = "mappings")]
		public ITypeMapping Mappings { get; internal set; }

		[DataMember(Name ="settings")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		public DynamicBody Settings { get; internal set; }

		[DataMember(Name ="state")]
		public string State { get; internal set; }
	}
}
