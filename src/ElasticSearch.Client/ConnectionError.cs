using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

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
		public string ExceptionMessage { get; set; }
		public Exception OriginalException { get; set; }

		public ConnectionError(Exception e)
		{
			this.OriginalException = e;
			this.ExceptionMessage = e.Message;
			this.Type = ConnectionErrorType.Uncaught;
			
			var webException = e as WebException;
			if (webException != null)
			{
				this.Type = ConnectionErrorType.Server;
				this.HttpStatusCode = ((HttpWebResponse)webException.Response).StatusCode;
				using (var responseStream = ((HttpWebResponse)webException.Response).GetResponseStream())
				using (var reader = new StreamReader(responseStream, true))
				{
					var response = reader.ReadToEnd();
					var x = new { Error = ""};
					x = JsonConvert.DeserializeAnonymousType(response, x);
					this.Message = x.Error;

				}

			}


		}
	}

}
