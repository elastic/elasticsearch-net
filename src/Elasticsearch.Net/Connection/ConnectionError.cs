using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Elasticsearch.Net.Connection
{

	public class ConnectionError
	{
		public HttpStatusCode? HttpStatusCode { get; set; }
		public string ExceptionMessage { get; set; }
		public Exception OriginalException { get; set; }

		public ConnectionError(Exception e)
		{
			this.OriginalException = e;
			this.ExceptionMessage = e.Message;
			var webException = e as WebException;
			if (webException == null)
			{
				var connectionException = e as ConnectionException;
				if (connectionException == null)
					return;
				this.HttpStatusCode = (HttpStatusCode)connectionException.HttpStatusCode;
				return;
			}
			//var response = (HttpWebResponse)webException.Response;
			//this.SetWebResponseData(response);
		}

		private void SetWebResponseData(HttpWebResponse response)
		{
			
		}
		
	}

}
