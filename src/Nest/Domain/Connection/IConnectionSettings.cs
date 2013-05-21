using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IConnectionSettings
	{
		FluentDictionary<Type, string> DefaultIndices { get; }
		FluentDictionary<Type, string> DefaultTypeNames { get; }
		string DefaultIndex { get; }

		string Host { get; }
		int Port { get; }
		int Timeout { get; }
		string ProxyAddress { get; }
		string ProxyUsername { get; }
		string ProxyPassword { get; }

		int MaximumAsyncConnections { get; }

		bool TraceEnabled { get; }

		bool UsesPrettyResponses { get; }
		Uri Uri { get; }

		Func<Type, string> DefaultTypeNameInferrer { get; }
	}
}
