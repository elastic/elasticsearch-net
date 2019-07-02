using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetIndexSettingsResponse, IndexName, IndexState>))]
	public class GetIndexSettingsResponse : DictionaryResponseBase<IndexName, IndexState>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, IndexState> Indices => Self.BackingDictionary;
	}
}
