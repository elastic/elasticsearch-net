using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Nest
{
	public interface IConnectionSettings
	{
		FluentDictionary<Type, string> DefaultIndices { get; }
		string DefaultIndex { get; }

		string Host { get; }
		int Port { get; }
		int Timeout { get; }
        IWebProxy Proxy { get; }
		string Username { get; }
		string Password { get; }

		int MaximumAsyncConnections { get; }
		bool UsesPrettyResponses { get; }
		Uri Uri { get; }
	}
}
