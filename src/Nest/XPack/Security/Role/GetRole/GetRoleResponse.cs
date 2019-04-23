using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetRoleResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackRole> Roles { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(DictionaryResponseJsonConverter<GetRoleResponse, string, XPackRole>))]
	public class GetRoleResponse : DictionaryResponseBase<string, XPackRole>, IGetRoleResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, XPackRole> Roles => Self.BackingDictionary;
	}
}
