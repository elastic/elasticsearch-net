using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetUserPrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackUserPrivileges> UserPrivilegess { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetUserPrivilegesResponse, string, XPackUserPrivileges>))]
	public class GetUserPrivilegesResponse : DictionaryResponseBase<string, XPackUserPrivileges>, IGetUserPrivilegesResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, XPackUserPrivileges> UserPrivilegess => Self.BackingDictionary;
	}

	public class XPackUserPrivileges
	{
		[JsonProperty("email")]
		public string Email { get; internal set; }

		[JsonProperty("full_name")]
		public string FullName { get; internal set; }

		[JsonProperty("metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		[JsonProperty("roles")]
		public IReadOnlyCollection<string> Roles { get; internal set; } = EmptyReadOnly<string>.Collection;

		[JsonProperty("username")]
		public string UserPrivilegesname { get; internal set; }
	}
}
