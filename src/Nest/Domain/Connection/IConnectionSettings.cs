using System;
using System.Collections.Generic;
using System.Linq;
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
		string ProxyAddress { get; }
		string Username { get; }
		string Password { get; }

		int MaximumAsyncConnections { get; }
		bool UsesPrettyResponses { get; }
		Uri Uri { get; }
	}
}
