using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetPrivilegesResponse, string, IDictionary<string, PrivilegesActions>>))]
	public class GetPrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, PrivilegesActions>>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IDictionary<string, PrivilegesActions>> Applications => Self.BackingDictionary;
	}
}
