using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	public class MetadataIndexState
	{
		[DataMember(Name = "aliases")]
		public IEnumerable<string> Aliases { get; internal set; }

		[DataMember(Name = "mappings")]
		public ITypeMapping Mappings { get; internal set; }

		// TODO: Why this uses DynamicBody
		[DataMember(Name = "settings")]
		public DynamicBody Settings { get; internal set; }

		[DataMember(Name = "state")]
		public string State { get; internal set; }
	}
}
