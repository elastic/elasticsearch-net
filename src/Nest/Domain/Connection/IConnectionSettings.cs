using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Nest
{
  public interface IConnectionSettings
  {
    FluentDictionary<Type, string> DefaultIndices { get; }
    FluentDictionary<Type, string> DefaultTypeNames { get; }
    string DefaultIndex { get; }

    Uri Uri { get; }
    int MaximumAsyncConnections { get; }
    string Host { get; }
    int Port { get; }
    int Timeout { get; }
    string ProxyAddress { get; }
    string ProxyUsername { get; }
    string ProxyPassword { get; }

    bool TraceEnabled { get; }
    bool DontDoubleEscapePathDotsAndSlashes { get; }
    NameValueCollection QueryStringParameters { get; }
    Action<ConnectionStatus> ConnectionStatusHandler { get; }

    Func<string, string> DefaultPropertyNameInferrer { get; }

    Func<Type, string> DefaultTypeNameInferrer { get; }

	Action<JsonSerializerSettings> ModifyJsonSerializerSettings { get; }

	ReadOnlyCollection<JsonConverter> ExtraConverters { get; }
  }
}
