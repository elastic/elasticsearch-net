using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetPrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, IDictionary<string, PrivilegesActions>> Applications { get; }
	}

	[JsonFormatter(typeof(DictionaryResponseFormatter<GetPrivilegesResponse, string, IDictionary<string, PrivilegesActions>>))]
	public class GetPrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, PrivilegesActions>>, IGetPrivilegesResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IDictionary<string, PrivilegesActions>> Applications => Self.BackingDictionary;
	}
}
