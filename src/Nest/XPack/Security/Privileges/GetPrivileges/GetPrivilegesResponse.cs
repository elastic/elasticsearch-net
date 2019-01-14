using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetPrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackPrivileges> Privilegess { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetPrivilegesResponse, string, XPackPrivileges>))]
	public class GetPrivilegesResponse : DictionaryResponseBase<string, XPackPrivileges>, IGetPrivilegesResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, XPackPrivileges> Privilegess => Self.BackingDictionary;
	}

	public class XPackPrivileges
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
		public string Privilegesname { get; internal set; }
	}
}
