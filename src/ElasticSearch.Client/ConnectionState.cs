using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ElasticSearch.Client
{
	class ConnectionState
	{
		public Action<ConnectionStatus> Callback { get; set; }
		public HttpWebRequest Connection { get; set; }

		public bool RaisedCallback { get; set; }

		public string PostData { get; set; }

		const int BUFFER_SIZE = 1024;
		public StringBuilder RequestData { get; set; }
		public byte[] BufferRead { get; set; }
		public HttpWebResponse Response { get; set; }
		public Stream StreamResponse { get; set; }
		public ConnectionState()
		{
			BufferRead = new byte[BUFFER_SIZE];
			RequestData = new StringBuilder("");
			Connection = null;
			StreamResponse = null;
		}
		public void RaiseCallBack(Exception e)
		{
			var error = new ConnectionError(e);

			if (e is WebException)
			{
				error.Type = ConnectionErrorType.Server;
				var oe = (WebException)e;
				if (oe.Response != null)
				{
					error.HttpStatusCode = ((System.Net.HttpWebResponse)(oe.Response)).StatusCode;

				}
				else
				{
					error.ExceptionMessage = "Could not connect to server: " + Connection.Address.ToString();
				}
			}
			else if (e is Exception)
				error.Type = ConnectionErrorType.Client;

			if (!this.RaisedCallback)
			{
				this.RaisedCallback = true;
				if (this.Callback != null)
					this.Callback(new ConnectionStatus(error));
			}
		}
		public void RaiseCallBack()
		{
			if (!this.RaisedCallback && this.Callback != null)
				this.Callback(new ConnectionStatus(this.RequestData.ToString()));
		}
	}
}
