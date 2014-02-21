using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Specialized;

namespace Elasticsearch.Net
{
	public class ElasticsearchConnectionSettings : 
		ElasticsearchConnectionSettings<ElasticsearchConnectionSettings>, 
		IElasticsearchConnectionSettings<ElasticsearchConnectionSettings>
	{
		public ElasticsearchConnectionSettings(Uri uri = null)
			: base(uri)
		{

		}
	}


	public class ElasticsearchConnectionSettings<T> : IConnectionSettings2
		where T : ElasticsearchConnectionSettings<T>
	{

		public Uri Uri { get; private set; }
		public string Host { get; private set; }
		public int Port { get; private set; }
		public int Timeout { get; private set; }
		public string ProxyUsername { get; private set; }
		public string ProxyPassword { get; private set; }
		public string ProxyAddress { get; private set; }
		public int MaximumAsyncConnections { get; private set; }
		public bool UsesPrettyResponses { get; private set; }
		public bool TraceEnabled { get; private set; }
		public Action<ElasticsearchResponse> ConnectionStatusHandler { get; private set; }
		public NameValueCollection QueryStringParameters { get; private set; }
		public bool UriSpecifiedBasicAuth { get; private set; }
		IElasticsearchSerializer IConnectionSettings2.Serializer { get; set; }

		public ElasticsearchConnectionSettings(Uri uri = null)
		{
			this.Timeout = 60*1000;
			this.Uri = uri ?? new Uri("http://localhost:9200");

			//this makes sure that paths stay relative i.e if the root uri is:
			//http://my-saas-provider.com/instance
			if (!this.Uri.OriginalString.EndsWith("/"))
				this.Uri = new Uri(uri.OriginalString + "/");
			this.Host = uri.Host;
			this.Port = uri.Port;
			this.UriSpecifiedBasicAuth = !uri.UserInfo.IsNullOrEmpty();

			this.ConnectionStatusHandler = this.ConnectionStatusDefaultHandler;
			this.MaximumAsyncConnections = 0;
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

