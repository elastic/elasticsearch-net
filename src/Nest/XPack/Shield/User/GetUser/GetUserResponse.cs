using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetUserResponse : IResponse
	{
		IDictionary<string, User> Users { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetUserResponse, string, User>))]
	public class GetUserResponse : DictionaryResponseBase<string, User>, IGetUserResponse
	{
		[JsonIgnore]
		public IDictionary<string, User> Users => Self.BackingDictionary;
	}

	public class User
	{
		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("roles")]
		public IEnumerable<string> Roles { get; set; }

		[JsonProperty("full_name")]
		public string FullName { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("metadata")]
		public IDictionary<string, object> Metadata { get; set; }
	}
}
