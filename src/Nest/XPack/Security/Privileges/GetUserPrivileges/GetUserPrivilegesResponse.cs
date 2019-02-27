using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetUserPrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, IDictionary<string, UserPrivileges>> Applications { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetUserPrivilegesResponse, string, IDictionary<string, UserPrivileges>>))]
	public class GetUserPrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, UserPrivileges>>, IGetUserPrivilegesResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, IDictionary<string, UserPrivileges>> Applications => Self.BackingDictionary;
	}

	public class UserPrivileges
	{
		[JsonProperty("application")]
		public string Application { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("actions")]
		public IReadOnlyCollection<string> Actions { get; internal set; } = EmptyReadOnly<string>.Collection;

		[JsonProperty("metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
