using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeletePrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, IDictionary<string, FoundUserPrivilege>> Applications { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<DeletePrivilegesResponse, string, IDictionary<string, FoundUserPrivilege>>))]
	public class DeletePrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, FoundUserPrivilege>>, IDeletePrivilegesResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, IDictionary<string, FoundUserPrivilege>> Applications => Self.BackingDictionary;
	}

	public class FoundUserPrivilege
	{
		[JsonProperty("found")]
		public bool Found { get; internal set; }
	}
}
