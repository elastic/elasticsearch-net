using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection
{
	public class ConnectionConfiguration : 
		ConnectionConfiguration<ConnectionConfiguration>, 
		IConnectionConfiguration<ConnectionConfiguration>
	{
		public ConnectionConfiguration(Uri uri = null)
			: base(uri)
		{

		}
		public ConnectionConfiguration(IConnectionPool connectionPool)
			: base(connectionPool)
		{

		}
	}


	public class ConnectionConfiguration<T> : IConnectionConfigurationValues
		where T : ConnectionConfiguration<T>
	{
		public IConnectionPool ConnectionPool { get; private set; }
		//public Uri Uri { get; private set; }
		//public string Host { get; private set; }
		//public int Port { get; private set; }
		public int Timeout { get; private set; }
		public string ProxyUsername { get; private set; }
		public string ProxyPassword { get; private set; }
		public string ProxyAddress { get; private set; }
		public int MaximumAsyncConnections { get; private set; }
		public int? MaxRetries { get; private set; }
		public bool UsesPrettyResponses { get; private set; }
		public bool SniffsOnStartup { get; private set; }
		public bool SniffsOnConnectionFault { get; private set; }
		public TimeSpan? SniffInformationLifeSpan { get; private set; }
		public bool TraceEnabled { get; private set; }
		public Action<ElasticsearchResponse> ConnectionStatusHandler { get; private set; }
		public NameValueCollection QueryStringParameters { get; private set; }
		public bool UriSpecifiedBasicAuth { get; private set; }
		IElasticsearchSerializer IConnectionConfigurationValues.Serializer { get; set; }

		public ConnectionConfiguration(IConnectionPool connectionPool)
		{
			this.Timeout = 60*1000;
			//this.UriSpecifiedBasicAuth = !uri.UserInfo.IsNullOrEmpty();
			//this.Uri = uri;
			this.ConnectionStatusHandler = this.ConnectionStatusDefaultHandler;
			this.MaximumAsyncConnections = 0;
			this.ConnectionPool = connectionPool;
		}

		public ConnectionConfiguration(Uri uri = null) 
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200")))
		{
			//this.Host = uri.Host;
			//this.Port = uri.Port;
		}

		public T SetMaxRetries(int maxRetries)
		{
			this.MaxRetries = maxRetries;
			return (T) this;
		}

		public T SnifsOnConnectionFault(bool sniffsOnConnectionFault = true)
		{
			this.SniffsOnConnectionFault = sniffsOnConnectionFault;
			return (T)this;
		}
		public T SniffOnStartup(bool sniffsOnStartup = true)
		{
			this.SniffsOnStartup = sniffsOnStartup;
			return (T)this;
		}
		public T SniffLifeSpan(TimeSpan sniffTimeSpan)
		{
			this.SniffInformationLifeSpan = sniffTimeSpan;
			return (T)this;
		}

		/// <summary>
		/// Enable Trace signals to the IConnection that it should put debug information on the Trace.
		/// </summary>
		public T EnableTrace(bool enabled = true)
		{
			this.TraceEnabled = enabled;
			return (T) this;
		}

		/// <summary>
		/// This NameValueCollection will be appended to every url NEST calls, great if you need to pass i.e an API key.
		/// </summary>
		public T SetGlobalQueryStringParameters(NameValueCollection queryStringParameters)
		{
			if (this.QueryStringParameters != null)
			{
				this.QueryStringParameters.Add(queryStringParameters);
			}
			this.QueryStringParameters = queryStringParameters;
			return (T) this;
		}

		/// <summary>
		/// Timeout in milliseconds when the .NET webrquest should abort the request, note that you can set this to a high value here,
		/// and specify the timeout in various calls on Elasticsearch's side.
		/// </summary>
		/// <param name="timeout">time out in milliseconds</param>
		public T SetTimeout(int timeout)
		{
			this.Timeout = timeout;
			return (T) this;
		}

		/// <summary>
		/// Semaphore asynchronous connections automatically by giving
		/// it a maximum concurrent connections. 
		/// </summary>
		/// <param name="maximum">defaults to 0 (unbounded)</param>
		public T SetMaximumAsyncConnections(int maximum)
		{
			this.MaximumAsyncConnections = maximum;
			return (T) this;
		}

		/// <summary>
		/// If your connection has to go through proxy use this method to specify the proxy url
		/// </summary>
		public T SetProxy(Uri proxyAdress, string username, string password)
		{
			proxyAdress.ThrowIfNull("proxyAdress");
			this.ProxyAddress = proxyAdress.ToString();
			this.ProxyUsername = username;
			this.ProxyPassword = password;
			return (T) this;
		}

		/// <summary>
		/// Append ?pretty=true to requests, this helps to debug send and received json.
		/// </summary>
		public T UsePrettyResponses(bool b = true)
		{
			this.UsesPrettyResponses = b;
			this.SetGlobalQueryStringParameters(new NameValueCollection {{"pretty", b.ToString().ToLowerInvariant()}});
			return (T) this;
		}

		protected void ConnectionStatusDefaultHandler(ElasticsearchResponse status)
		{
			return;
		}

		/// <summary>
		/// Global callback for every response that NEST receives, useful for custom logging.
		/// </summary>
		public T SetConnectionStatusHandler(Action<ElasticsearchResponse> handler)
		{
			handler.ThrowIfNull("handler");
			this.ConnectionStatusHandler = handler;
			return (T) this;
		}
	}
}

