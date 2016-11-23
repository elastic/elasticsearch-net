using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAuthenticateResponse : IResponse
	{
		[JsonProperty("username")]
		string Username { get; }

		[JsonProperty("roles")]
		IReadOnlyCollection<string> Roles { get; }

		[JsonProperty("full_name")]
		string FullName { get; }

		[JsonProperty("email")]
		string Email { get; }

		[JsonProperty("metadata")]
		IReadOnlyDictionary<string, object> Metadata { get; }

	}

	public class AuthenticateResponse : ResponseBase, IAuthenticateResponse
	{
		public string Username { get; internal set; }

		public IReadOnlyCollection<string> Roles { get; internal set; } = EmptyReadOnly<string>.Collection;

		public string FullName { get; internal set;  }

		public string Email { get; internal set;  }

		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
