using System;
using System.Net;

namespace Elasticsearch.Net.Connection
{
	public class ConnectionException : System.Exception
	{
		public int HttpStatusCode { get; private set; }
		public ConnectionException(int statusCode = 500, string response = null) : base(Enum.GetName(typeof(HttpStatusCode), statusCode))
		{
			this.HttpStatusCode = statusCode;
		}
	}
}