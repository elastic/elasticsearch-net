using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAuthenticateResponse : IResponse
	{
		[JsonProperty("username")]
		string Username { get; }

		[JsonProperty("roles")]
		IEnumerable<string> Roles { get; }

		[JsonProperty("full_name")]
		string FullName { get; }

		[JsonProperty("email")]
		string Email { get; }

		[JsonProperty("metadata")]
		IDictionary<string, object> Metadata { get; }

	}

	public class AuthenticateResponse : ResponseBase, IAuthenticateResponse
	{
		public string Username { get; internal set; }

		public IEnumerable<string> Roles { get; internal set;  }

		public string FullName { get; internal set;  }

		public string Email { get; internal set;  }

		public IDictionary<string, object> Metadata { get; internal set;  }

	}
}
