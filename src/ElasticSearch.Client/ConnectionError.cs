using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ElasticSearch.Client
{
	public enum ConnectionErrorType
	{
		Uncaught,
		Client,
		Server,
		UnAuthorizedAccess
	}

	public class ConnectionError
	{
		public ConnectionErrorType Type { get; set; }
		public HttpStatusCode HttpStatusCode { get; set; }
		public string Message { get; set; }
		public Exception OriginalException { get; set; }
	}

}
