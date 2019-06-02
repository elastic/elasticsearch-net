using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetRoleResponse, string, XPackRole>))]
	public class GetRoleResponse : DictionaryResponseBase<string, XPackRole>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, XPackRole> Roles => Self.BackingDictionary;
	}
}
