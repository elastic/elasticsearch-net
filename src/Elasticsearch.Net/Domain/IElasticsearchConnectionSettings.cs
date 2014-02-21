using System;
using System.Collections.Specialized;

namespace Elasticsearch.Net
{
	public interface IConnectionSettings2
	{
		Uri Uri { get; }
		int MaximumAsyncConnections { get; }
		string Host { get; }
		int Port { get; }
		int Timeout { get; }
		string ProxyAddress { get; }
		string ProxyUsername { get; }
		string ProxyPassword { get; }

		bool TraceEnabled { get; }
		bool UriSpecifiedBasicAuth { get; }
		bool UsesPrettyResponses { get; }
	
		NameValueCollection QueryStringParameters { get; }
		Action<ElasticsearchResponse> ConnectionStatusHandler { get; }

		IElasticsearchSerializer Serializer { get; set; }
	}

	public interface IElasticsearchConnectionSettings : IElasticsearchConnectionSettings<IElasticsearchConnectionSettings>
	{
		
	}

	public interface IElasticsearchConnectionSettings<out T> where T : IElasticsearchConnectionSettings<T>
	{

	
		/// <summary>
		/// Enable Trace signals to the IConnection that it should put debug information on the Trace.
		/// </summary>
		T EnableTrace(bool enabled = true);
		
		/// <summary>
		/// This NameValueCollection will be appended to every url NEST calls, great if you need to pass i.e an API key.
		/// </summary>
		/// <param name="queryStringParameters"></param>
		/// <returns></returns>
		T SetGlobalQueryStringParameters(NameValueCollection queryStringParameters);	/// <summary>
	
		/// Timeout in milliseconds when the .NET webrquest should abort the request, note that you can set this to a high value here,
		/// and specify the timeout in various calls on Elasticsearch's side.
		/// </summary>
		/// <param name="timeout">time out in milliseconds</param>
		T SetTimeout(int timeout);
		
		/// <summary>
		/// If your connection has to go through proxy use this method to specify the proxy url
		/// </summary>
		/// <returns></returns>
		T SetProxy(Uri proxyAdress, string username, string password);

		/// <summary>
		/// Append ?pretty=true to requests, this helps to debug send and received json.
		/// </summary>
		/// <returns></returns>
		T UsePrettyResponses(bool b = true);
		
		/// <summary>
		/// Semaphore asynchronous connections automatically by giving
		/// it a maximum concurrent connections. Great to prevent 
		/// out of memory exceptions
		/// </summary>
		/// <param name="maximum">defaults to 20</param>
		/// <returns></returns>
		T SetMaximumAsyncConnections(int maximum);
		
		/// <summary>
		/// Global callback for every response that NEST receives, useful for custom logging.
		/// </summary>
		T SetConnectionStatusHandler(Action<ElasticsearchResponse> handler);
	}
}