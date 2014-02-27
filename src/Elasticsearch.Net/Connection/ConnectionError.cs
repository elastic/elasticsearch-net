using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Elasticsearch.Net.Connection
{
	public enum ConnectionErrorType
	{
		/// <summary>
		/// The error was due to an uncaught exception in the client code
		/// </summary>
		Uncaught,
		/// <summary>
		/// The error was due to an error thrown by Elasticsearch
		/// </summary>
		Server,
	}

	public class ConnectionError
	{
		public ConnectionErrorType Type { get; set; }
		public HttpStatusCode HttpStatusCode { get; set; }
		public string ExceptionMessage { get; set; }
		public Exception OriginalException { get; set; }
		public byte[] ResponseReadFromWebException { get; set; }

		public ConnectionError(Exception e)
		{
			this.OriginalException = e;
			this.ExceptionMessage = e.Message;
			this.Type = ConnectionErrorType.Uncaught;
			var webException = e as WebException;
			if (webException == null)
			{
				var connectionException = e as ConnectionException;
				if (connectionException == null)
					return;
				this.HttpStatusCode = (HttpStatusCode)connectionException.HttpStatusCode;
				return;
			}
			this.Type = ConnectionErrorType.Server;
			var response = (HttpWebResponse)webException.Response;
			this.SetWebResponseData(response);
		}

		private void SetWebResponseData(HttpWebResponse response)
		{
			if (response == null)
				return;

			this.HttpStatusCode = response.StatusCode;
			try
			{
				using (var responseStream = response.GetResponseStream())
				using (var memoryStream = new MemoryStream())
				{
					responseStream.CopyTo(memoryStream);
					this.ResponseReadFromWebException = memoryStream.ToArray();
				}
			}
			finally 
			{ 
			}
		}
		
	}

}
