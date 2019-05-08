using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAuthenticateResponse : IResponse
	{
		[JsonProperty("email")]
		string Email { get; }

		[JsonProperty("full_name")]
		string FullName { get; }

		[JsonProperty("metadata")]
		IReadOnlyDictionary<string, object> Metadata { get; }

		[JsonProperty("roles")]
		IReadOnlyCollection<string> Roles { get; }

		[JsonProperty("username")]
		string Username { get; }

		[JsonProperty("authentication_realm")]
		RealmInfo AuthenticationRealm { get; }

		[JsonProperty("lookup_realm")]
		RealmInfo LookupRealm { get; }
	}

	public class AuthenticateResponse : ResponseBase, IAuthenticateResponse
	{
		public string Email { get; internal set; }

		public string FullName { get; internal set; }

		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		public IReadOnlyCollection<string> Roles { get; internal set; } = EmptyReadOnly<string>.Collection;

		public string Username { get; internal set; }

		public RealmInfo AuthenticationRealm { get; internal set; }

		public RealmInfo LookupRealm { get; internal set; }
	}

	public class RealmInfo
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }
	}
}
