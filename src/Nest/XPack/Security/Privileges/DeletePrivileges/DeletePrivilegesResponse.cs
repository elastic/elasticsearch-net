using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IDeletePrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, IDictionary<string, FoundUserPrivilege>> Applications { get; }
	}

	[JsonFormatter(typeof(DictionaryResponseFormatter<DeletePrivilegesResponse, string, IDictionary<string, FoundUserPrivilege>>))]
	public class DeletePrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, FoundUserPrivilege>>, IDeletePrivilegesResponse
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
