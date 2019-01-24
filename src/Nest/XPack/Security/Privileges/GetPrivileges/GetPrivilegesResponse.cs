using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetPrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, IDictionary<string, PrivilegesActions>> Applications { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetPrivilegesResponse, string, IDictionary<string, PrivilegesActions>>))]
	public class GetPrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, PrivilegesActions>>, IGetPrivilegesResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, IDictionary<string, PrivilegesActions>> Applications => Self.BackingDictionary;
	}
}
