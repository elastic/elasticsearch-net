using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class ConnectionSettings : IConnectionSettings
	{
		private readonly string _username;
		public string Username
		{
			get { return this._username; }
		}
		private readonly string _password;
		public string Password
		{
			get { return this._password; }
		}
		private readonly string _host;
		public string Host
		{
			get { return this._host; }
		}
		private readonly string _proxyAddress;
		public string ProxyAddress
		{
			get { return this._proxyAddress; }
		}
		private readonly int _port;
		public int Port
		{
			get { return this._port; }
		}
		private int _timeout;
		public int Timeout
		{
			get { return this._timeout; }
		}
		private string _defaultIndex;
		public string DefaultIndex
		{
			get
			{
				if (this._defaultIndex.IsNullOrEmpty())
					throw new NullReferenceException("No default index set on connection!");
				return this._defaultIndex;
			}
			private set { this._defaultIndex = value; }
		}
		private readonly Uri _uri;
		public Uri Uri
		{
			get { return this._uri; }
		}


		public int MaximumAsyncConnections { get; private set; }
		public bool UsesPrettyResponses { get; private set; }

		private readonly FluentDictionary<Type, string> _defaultTypeIndices;

		public FluentDictionary<Type, string> DefaultIndices
		{
			get
			{
				return this._defaultTypeIndices;
			}
		}

		/// <summary>
		/// Instantiate a connectionsettings object to tell the client where and how to connect to elasticsearch
		/// </summary>
		/// <param name="uri">A Uri to describe the elasticsearch endpoint</param>
		public ConnectionSettings(Uri uri) : this(uri, 60000, null, null, null) { }
		/// <summary>
		/// Instantiate a connectionsettings object to tell the client where and how to connect to elasticsearch
		/// </summary>
		/// <param name="uri">A Uri to describe the elasticsearch endpoint</param>
		/// <param name="timeout">time out in milliseconds</param>
		public ConnectionSettings(Uri uri, int timeout) : this(uri, timeout, null, null, null) { }
		/// <summary>
		/// Instantiate a connectionsettings object to tell the client where and how to connect to elasticsearch
		/// using a proxy
		/// </summary>
		/// <param name="uri">A Uri to describe the elasticsearch endpoint</param>
		/// <param name="timeout">time out in milliseconds</param>
		/// <param name="proxyAddress">proxy address</param>
		/// <param name="username">proxy username</param>
		/// <param name="password">proxy password</param>
		public ConnectionSettings(Uri uri, int timeout, string proxyAddress, string username, string password)
		{
			uri.ThrowIfNull("uri");

			this._uri = uri;
		    this._host = uri.Host;
		    this._port = uri.Port;
			this._password = password;
			this._username = username;
			this._timeout = timeout;
			this._proxyAddress = proxyAddress;
			this.MaximumAsyncConnections = 20;
			this._defaultTypeIndices = new FluentDictionary<Type, string>();
		}
		/// <summary>
		/// Instantiate a connectionsettings object to tell the client where and how to connect to elasticsearch
		/// </summary>
		/// <param name="host">host (sans http(s)://), use the Uri constructor overload for more control</param>
		/// <param name="port">port of the host (elasticsearch defaults on 9200)</param>
		public ConnectionSettings(string host, int port) : this(host, port, 60000, null, null, null) { }
		/// <summary>
		/// Instantiate a connectionsettings object to tell the client where and how to connect to elasticsearch
		/// </summary>
		/// <param name="host">host (sans http(s)://), use the Uri constructor overload for more control</param>
		/// <param name="port">port of the host (elasticsearch defaults on 9200)</param>
		/// <param name="timeout">time out in milliseconds</param>
		public ConnectionSettings(string host, int port, int timeout) : this(host, port, timeout, null, null, null) { }
		/// <summary>
		/// Instantiate a connectionsettings object to tell the client where and how to connect to elasticsearch
		/// </summary>
		/// <param name="host">host (sans http(s)://), use the Uri constructor overload for more control</param>
		/// <param name="port">port of the host (elasticsearch defaults on 9200)</param>
		/// <param name="timeout">time out in milliseconds</param>
		/// <param name="proxyAddress">proxy address</param>
		/// <param name="username">proxy username</param>
		/// <param name="password">proxy password</param>
		public ConnectionSettings(string host, int port, int timeout, string proxyAddress, string username, string password)
		{
			host.ThrowIfNullOrEmpty("host");
			var uri = new Uri("http://" + host + ":" + port);

			this._host = host;
			this._password = password;
		    this._uri = uri;
			this._username = username;
			this._timeout = timeout;
			this._port = port;
			this._proxyAddress = proxyAddress;
			this.MaximumAsyncConnections = 20;
			this._defaultTypeIndices = new FluentDictionary<Type, string>();
		}
		/// <summary>
		/// Index to default to when no index is specified.
		/// </summary>
		/// <param name="defaultIndex">When null/empty/not set might throw NRE later on
		/// when not specifying index explicitly while indexing.
		/// </param>
		/// <returns></returns>
		public ConnectionSettings SetDefaultIndex(string defaultIndex)
		{
			this.DefaultIndex = defaultIndex;
			return this;
		}
		/// <summary>
		/// Semaphore asynchronous connections automatically by giving
		/// it a maximum concurrent connections. Great to prevent 
		/// out of memory exceptions
		/// </summary>
		/// <param name="maximum">defaults to 20</param>
		/// <returns></returns>
		public ConnectionSettings SetMaximumAsyncConnections(int maximum)
		{
			this.MaximumAsyncConnections = maximum;
			return this;
		}
		/// <summary>
		/// Timeout in milliseconds when the .NET webrquest should abort the request, note that you can set this to a high value here,
		/// and specify the timeout in various calls on Elasticsearch's side.
		/// </summary>
		/// <param name="timeout">time out in milliseconds</param>
		public ConnectionSettings SetTimeout(int timeout)
		{
			this._timeout = timeout;
			return this;
		}
		public ConnectionSettings UsePrettyResponses()
		{
			this.UsesPrettyResponses = true;
			return this;
		}
		public ConnectionSettings UsePrettyResponses(bool b)
		{
			this.UsesPrettyResponses = b;
			return this;
		}

		public ConnectionSettings MapTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");			
			mappingSelector(this._defaultTypeIndices);
			return this;
		}

		
	}
}
