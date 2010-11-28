using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public class ConnectionSettings : IConnectionSettings
	{
		private readonly string _username;
		public string Username {
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
		private readonly int _timeOut;
		public int TimeOut
		{
			get { return this._timeOut; }
		}
		public string DefaultIndex { get; private set; }
		public int MaximumAsyncConnections { get; private set; }
		
		public ConnectionSettings(string host, int port) : this(host, port, 60000, null, null, null) { }
		public ConnectionSettings(string host, int port, int timeout) : this (host, port, timeout, null, null, null) {}
		public ConnectionSettings(string host, int port, int timeout, string proxyAddress, string username, string password)
		{
			this._host = host;
			this._password = password;
			this._username = username;
			this._timeOut = timeout;
			this._port = port;
			this._proxyAddress = proxyAddress;
		}
		
		public ConnectionSettings SetDefaultIndex(string defaultIndex)
		{
			this.DefaultIndex = defaultIndex;
			return this;
		}
		public ConnectionSettings SetMaximumAsyncConnections(int maximum)
		{
			this.MaximumAsyncConnections = maximum;
			return this;
		}
		
		
	}
}
