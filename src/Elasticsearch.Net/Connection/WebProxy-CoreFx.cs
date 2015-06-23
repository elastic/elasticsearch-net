using System;
using System.Net;

namespace Elasticsearch.Net.Connection
{

	// might be overly complicated, there is an option to wrap the existing proxy here...
	public class ClientConnectionProxy : System.Net.IWebProxy
	{
		private ICredentials creds;
		private readonly Uri _proxyUri;

		private void init()
		{
			creds = CredentialCache.DefaultCredentials;
		}
		public ClientConnectionProxy(Uri uri)
		{
			init();
			this._proxyUri = uri;
		}

		public ICredentials Credentials
		{
			get { return creds; }
			set { creds = value; }
		}

		public Uri GetProxy(Uri destination)
		{
			return _proxyUri;
		}

		public bool IsBypassed(Uri host)
		{
			return false;
		}
	}
}