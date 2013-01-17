using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Nest
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
		internal string Response { get; set; }

		public ConnectionError(Exception e)
		{
			this.OriginalException = e;
			this.ExceptionMessage = e.Message;
			this.Type = ConnectionErrorType.Uncaught;

			var webException = e as WebException;
			if (webException == null)
				return;

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
				using (var reader = new StreamReader(responseStream, true))
				{
					this.Response = reader.ReadToEnd();
					this.TryReadElasticsearchException();
				}
			}
			finally 
			{ 
			}
		}

		private void TryReadElasticsearchException()
		{
			var x = new { Error = string.Empty };
			if (string.IsNullOrWhiteSpace(this.Response))
				return;
			try
			{
				x = JsonConvert.DeserializeAnonymousType(this.Response, x);
				this.ExceptionMessage = x.Error;
			}
			catch
			{
				this.ExceptionMessage = "Could not parse exception message from ES got this back instead:\r\n" + this.Response;
			}
		}
	}

}
