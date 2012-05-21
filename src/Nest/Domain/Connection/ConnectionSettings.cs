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
		private readonly int _timeOut;
		public int TimeOut
		{
			get { return this._timeOut; }
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
		public Func<string, string> TypeNameInferrer { get; private set; }

        private FluentDictionary<Type, string> _defaultTypeIndices;

        public ConnectionSettings(Uri uri) : this(uri, 60000, null, null, null) { }
        public ConnectionSettings(Uri uri, int timeout) : this(uri, timeout, null, null, null) { }
        public ConnectionSettings(Uri uri, int timeout, string proxyAddress, string username, string password)
        {
            uri.ThrowIfNull("uri");

            this._uri = uri;
            this._password = password;
            this._username = username;
            this._timeOut = timeout;
            this._proxyAddress = proxyAddress;
            this.MaximumAsyncConnections = 20;
        }

		public ConnectionSettings(string host, int port) : this(host, port, 60000, null, null, null) { }
		public ConnectionSettings(string host, int port, int timeout) : this(host, port, timeout, null, null, null) { }
		public ConnectionSettings(string host, int port, int timeout, string proxyAddress, string username, string password)
		{
			host.ThrowIfNullOrEmpty("host");

			var uri = new Uri("http://" + host + ":" + port);


			this._host = host;
			this._password = password;
			this._username = username;
			this._timeOut = timeout;
			this._port = port;
			this._proxyAddress = proxyAddress;
			this.MaximumAsyncConnections = 20;
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
		public ConnectionSettings SetTypeNameInferrer(Func<string, string> inferrer)
		{
			this.TypeNameInferrer = inferrer;
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

      var dict = new FluentDictionary<Type, string>();
      mappingSelector(dict);
      this._defaultTypeIndices = dict;
      return this;
    }

    public string GetIndexForType<T>()
    {
      return this.GetIndexForType(typeof(T));
    }
    public string GetIndexForType(Type type)
    {
      if (this._defaultTypeIndices == null)
        return this.DefaultIndex;
      if (this._defaultTypeIndices.ContainsKey(type) && !string.IsNullOrWhiteSpace(this._defaultTypeIndices[type]))
        return this._defaultTypeIndices[type];
      return this.DefaultIndex;
    }
	}
}
