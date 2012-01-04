using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IConnectionSettings
	{
		string Host { get; }
		int Port { get; }
		int TimeOut { get; }
		string ProxyAddress { get; }
		string Username { get;  }
		string Password { get; }
		string DefaultIndex { get; }
		int MaximumAsyncConnections { get; }
		Func<string, string> TypeNameInferrer { get; }
		bool UsesPrettyResponses { get; }
	}
}
