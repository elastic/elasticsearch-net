using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class StoredScriptMapping
	{
		[DataMember(Name ="lang")]
		public string Language { get; internal set; }

		[DataMember(Name ="source")]
		public string Source { get; internal set; }

		[DataMember(Name ="options")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, string>))]
		public IReadOnlyDictionary<string, string> Options { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
