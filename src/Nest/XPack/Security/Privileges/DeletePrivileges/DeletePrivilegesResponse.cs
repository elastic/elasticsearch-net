using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<DeletePrivilegesResponse, string, IDictionary<string, FoundUserPrivilege>>))]
	public class DeletePrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, FoundUserPrivilege>>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IDictionary<string, FoundUserPrivilege>> Applications => Self.BackingDictionary;
	}

	public class FoundUserPrivilege
	{
		[DataMember(Name = "found")]
		public bool Found { get; internal set; }
	}
}
