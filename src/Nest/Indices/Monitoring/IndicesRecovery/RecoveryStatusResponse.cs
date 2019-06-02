using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<RecoveryStatusResponse, IndexName, RecoveryStatus>))]
	public class RecoveryStatusResponse : DictionaryResponseBase<IndexName, RecoveryStatus>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<IndexName, RecoveryStatus> Indices => Self.BackingDictionary;
	}
}
