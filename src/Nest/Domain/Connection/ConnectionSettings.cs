using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class ConnectionSettings : IConnectionSettings
	{
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
		public Uri Uri { get; private set; }
		public string Host { get; private set; }
		public int Port { get; private set; }
		public int Timeout { get; private set; }
		public string ProxyUsername { get; private set; }
		public string ProxyPassword { get; private set; }
		public string ProxyAddress { get; private set; }

		public int MaximumAsyncConnections { get; private set; }
		public bool UsesPrettyResponses { get; private set; }

		public FluentDictionary<Type, string> DefaultIndices { get; private set; }
		public FluentDictionary<Type, string> DefaultTypeNames { get; private set; }

		public ConnectionSettings(Uri uri)
		{
			uri.ThrowIfNull("uri");

			this.Timeout = 60*1000;

			this.Uri = uri;
			this.Host = uri.Host;
			this.Port = uri.Port;

			this.MaximumAsyncConnections = 20;
			this.DefaultIndices = new FluentDictionary<Type, string>();
			this.DefaultTypeNames = new FluentDictionary<Type, string>();
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
		/// If your connection has to go through proxy use this method to specify the proxy url
		/// </summary>
		/// <returns></returns>
		public ConnectionSettings SetProxy(Uri proxyAdress, string username, string password)
		{
			proxyAdress.ThrowIfNull("proxyAdress");
			this.ProxyAddress = proxyAdress.ToString();
			this.ProxyUsername = username;
			this.ProxyPassword = password;
			return this;
		}

		/// <summary>
		/// Timeout in milliseconds when the .NET webrquest should abort the request, note that you can set this to a high value here,
		/// and specify the timeout in various calls on Elasticsearch's side.
		/// </summary>
		/// <param name="timeout">time out in milliseconds</param>
		public ConnectionSettings SetTimeout(int timeout)
		{
			this.Timeout = timeout;
			return this;
		}

		/// <summary>
		/// Append ?pretty=true to requests, this helps to debug send and received json.
		/// </summary>
		/// <returns></returns>
		public ConnectionSettings UsePrettyResponses(bool b = true)
		{
			this.UsesPrettyResponses = b;
			return this;
		}
		
		public ConnectionSettings MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");			
			mappingSelector(this.DefaultIndices);
			return this;
		}
		public ConnectionSettings MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			mappingSelector(this.DefaultTypeNames);
			return this;
		}
		
	}
}
