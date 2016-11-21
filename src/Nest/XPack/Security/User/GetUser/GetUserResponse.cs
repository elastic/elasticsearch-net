using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetUserResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackUser> Users { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetUserResponse, string, XPackUser>))]
	public class GetUserResponse : DictionaryResponseBase<string, XPackUser>, IGetUserResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, XPackUser> Users => Self.BackingDictionary;
	}

	public class XPackUser
	{
		[JsonProperty("username")]
		public string Username { get; internal set; }

		[JsonProperty("roles")]
		public IReadOnlyCollection<string> Roles { get; internal set; } = EmptyReadOnly<string>.Collection;

		[JsonProperty("full_name")]
		public string FullName { get; internal set; }

		[JsonProperty("email")]
		public string Email { get; internal set; }

		[JsonProperty("metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
