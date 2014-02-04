using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Nest
{
	
	/// <summary>
	/// Control how NEST's behaviour.
	/// </summary>
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
		NameValueCollection QueryStringParameters { get; }
		Action<ConnectionStatus> ConnectionStatusHandler { get; }

		Func<string, string> DefaultPropertyNameInferrer { get; }

		Func<Type, string> DefaultTypeNameInferrer { get; }

		Action<JsonSerializerSettings> ModifyJsonSerializerSettings { get; }

		ReadOnlyCollection<Func<Type, JsonConverter>> ContractConverters { get; }
		bool UsesPrettyResponses { get; }

		/// <summary>
		/// Enable Trace signals to the IConnection that it should put debug information on the Trace.
		/// </summary>
		ConnectionSettings EnableTrace(bool enabled = true);

		/// <summary>
		/// This calls SetDefaultTypenameInferrer with an implementation that will pluralize type names. This used to be the default prior to Nest 0.90
		/// </summary>
		/// <returns></returns>
		ConnectionSettings PluralizeTypeNames();

		/// <summary>
		/// Allows you to update internal the json.net serializer settings to your liking
		/// </summary>
		/// <param name="modifier"></param>
		/// <returns></returns>
		ConnectionSettings SetJsonSerializerSettingsModifier(Action<JsonSerializerSettings> modifier);

		/// <summary>
		/// Add a custom JsonConverter to the build in json serialization by passing in a predicate for a type.
		/// This is faster then adding them using AddJsonConverters() because this way they will be part of the cached 
		/// Json.net contract for a type.
		/// </summary>
		ConnectionSettings AddContractJsonConverters(params Func<Type, JsonConverter>[] contractSelectors);

		/// <summary>
		/// This NameValueCollection will be appended to every url NEST calls, great if you need to pass i.e an API key.
		/// </summary>
		/// <param name="queryStringParameters"></param>
		/// <returns></returns>
		ConnectionSettings SetGlobalQueryStringParameters(NameValueCollection queryStringParameters);

		/// <summary>
		/// Timeout in milliseconds when the .NET webrquest should abort the request, note that you can set this to a high value here,
		/// and specify the timeout in various calls on Elasticsearch's side.
		/// </summary>
		/// <param name="timeout">time out in milliseconds</param>
		ConnectionSettings SetTimeout(int timeout);

		/// <summary>
		/// Index to default to when no index is specified.
		/// </summary>
		/// <param name="defaultIndex">When null/empty/not set might throw NRE later on
		/// when not specifying index explicitly while indexing.
		/// </param>
		/// <returns></returns>
		ConnectionSettings SetDefaultIndex(string defaultIndex);

		/// <summary>
		/// Semaphore asynchronous connections automatically by giving
		/// it a maximum concurrent connections. Great to prevent 
		/// out of memory exceptions
		/// </summary>
		/// <param name="maximum">defaults to 20</param>
		/// <returns></returns>
		ConnectionSettings SetMaximumAsyncConnections(int maximum);

		/// <summary>
		/// If your connection has to go through proxy use this method to specify the proxy url
		/// </summary>
		/// <returns></returns>
		ConnectionSettings SetProxy(Uri proxyAdress, string username, string password);

		/// <summary>
		/// Append ?pretty=true to requests, this helps to debug send and received json.
		/// </summary>
		/// <returns></returns>
		ConnectionSettings UsePrettyResponses(bool b = true);

		/// <summary>
		/// By default NEST camelCases property names (EmailAddress => emailAddress) that do not have an explicit propertyname 
		/// either via an ElasticProperty attribute or because they are part of Dictionary where the keys should be treated verbatim.
		/// <pre>
		/// Here you can register a function that transforms propertynames (default casing, pre- or suffixing)
		/// </pre>
		/// </summary>
		ConnectionSettings SetDefaultPropertyNameInferrer(Func<string, string> propertyNameSelector);

		/// <summary>
		/// Allows you to override how type names should be reprented, the default will call .ToLowerInvariant() on the type's name.
		/// </summary>
		ConnectionSettings SetDefaultTypeNameInferrer(Func<Type, string> defaultTypeNameInferrer);

		/// <summary>
		/// Global callback for every response that NEST receives, useful for custom logging.
		/// </summary>
		ConnectionSettings SetConnectionStatusHandler(Action<ConnectionStatus> handler);

		/// <summary>
		/// Map types to a index names. Takes precedence over SetDefaultIndex().
		/// </summary>
		ConnectionSettings MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector);

		/// <summary>
		/// Allows you to override typenames, takes priority over the global SetDefaultTypeNameInferrer()
		/// </summary>
		ConnectionSettings MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector);
	}
}
