using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetRoleResponse : IResponse
	{
		IDictionary<string, Role> Roles { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetRoleResponse, string, Role>))]
	public class GetRoleResponse : DictionaryResponseBase<string, Role>, IGetRoleResponse
	{
		[JsonIgnore]
		public IDictionary<string, Role> Roles => Self.BackingDictionary;
	}
}
