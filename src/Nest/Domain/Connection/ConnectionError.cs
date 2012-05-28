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
		Uncaught,
		Client,
		Server,
		UnAuthorizedAccess
	}

	public class ConnectionError
	{
		public ConnectionErrorType Type { get; set; }
		public HttpStatusCode HttpStatusCode { get; set; }
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
				var response = ((HttpWebResponse)webException.Response);
				if (response == null)
				{
					this.Type = ConnectionErrorType.Client;
				}
				else
				{
					this.HttpStatusCode = response.StatusCode;
					using (var responseStream = response.GetResponseStream())
					using (var reader = new StreamReader(responseStream, true))
					{
						var responseString = reader.ReadToEnd();
						var x = new { Error = "" };
						try
						{ 
							x = JsonConvert.DeserializeAnonymousType(responseString, x);
							this.ExceptionMessage = x.Error;
						}
						catch
						{
							this.ExceptionMessage = "Could not parse exception message from ES, possibly altered by proxy or this is an unhandled HTTP Status by ES\r\n" + responseString;
						}

					}
				}

			}


		}
		public ConnectionError(Exception e, string result)
		{
			this.OriginalException = e;
			this.ExceptionMessage = e.Message;
			this.Type = ConnectionErrorType.Uncaught;

			var webException = e as WebException;
			if (webException != null)
			{
				this.Type = ConnectionErrorType.Server;
				var response = ((HttpWebResponse)webException.Response);
				if (response == null)
				{
					this.Type = ConnectionErrorType.Client;
				}
				else
				{
					this.HttpStatusCode = response.StatusCode;
;
					var x = new { Error = "" };
					try
					{
						x = JsonConvert.DeserializeAnonymousType(result, x);
						this.ExceptionMessage = x.Error;
					}
					catch
					{
						this.ExceptionMessage = "Could not parse exception message from ES, possibly altered by proxy or this is an unhandled HTTP Status by ES\r\n" + result;
					}

				}

			}


		}
	}

}
