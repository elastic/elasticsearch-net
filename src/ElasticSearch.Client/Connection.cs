using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace ElasticSearch.Client
{
	internal class Connection : IConnection
	{
		public static ManualResetEvent allDone = new ManualResetEvent(false);
		const int BUFFER_SIZE = 1024;

		private IConnectionSettings _ConnectionSettings { get; set; }

		public Connection(IConnectionSettings settings)
		{
			this._ConnectionSettings = settings;
		}
		
		public ConnectionStatus GetSync(string path)
		{
			var connection = this.CreateConnection(path);
			connection.Timeout = this._ConnectionSettings.TimeOut;
			connection.Method = "GET";
			try
			{
				var response = connection.GetResponse();
				var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
				return new ConnectionStatus(result);
			}
			catch (WebException e) 
			{
				if (e.Status == WebExceptionStatus.Timeout)
					return new ConnectionStatus(new ConnectionError() { Type = ConnectionErrorType.Server, Message = "Timeout", OriginalException = e });
				return new ConnectionStatus(new ConnectionError() { HttpStatusCode = ((HttpWebResponse)e.Response).StatusCode, Message = e.Message, OriginalException = e, Type = ConnectionErrorType.Server }); 
			}
			catch (Exception e) { return new ConnectionStatus(new ConnectionError() { Type = ConnectionErrorType.Uncaught }); }
		}
		public ConnectionStatus PostSync(string path, string data)
		{
			var connection = this.CreateConnection(path);
			connection.Timeout = this._ConnectionSettings.TimeOut;
			connection.Method = "POST";
			try
			{
				byte[] buffer = Encoding.UTF8.GetBytes(data);
				connection.ContentLength = buffer.Length;
				var postStream = connection.GetRequestStream();
				postStream.Write(buffer, 0, buffer.Length);
				postStream.Close();
				var response = connection.GetResponse();
				var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
				return new ConnectionStatus(result);
			}
			catch (WebException e) { return new ConnectionStatus(new ConnectionError() { HttpStatusCode = ((HttpWebResponse)e.Response).StatusCode, Message = e.Message, OriginalException = e, Type = ConnectionErrorType.Server }); }
			catch (Exception e) { return new ConnectionStatus(new ConnectionError() { Type = ConnectionErrorType.Uncaught }); }
		}
		
		public void Get(string path, Action<ConnectionStatus> callback)
		{
			ConnectionState state = new ConnectionState()
			{
				Callback = callback,
				Connection = this.CreateConnection(path)
			};
			this.BeginGetResponse(state);
		}
		public void Post(string path, string data, Action<ConnectionStatus> callback)
		{
			this._PutOrPost("POST", path, data, callback);
		}
		public void Put(string path, string data, Action<ConnectionStatus> callback)
		{
			this._PutOrPost("PUT", path, data, callback);
		}

		private void _PutOrPost(string method, string path,string data, Action<ConnectionStatus> callback)
		{
			ConnectionState state = new ConnectionState()
			{
				Callback = callback,
				Connection = this.CreateConnection(path),
				PostData = data
			};
			state.Connection.Method = method;

			state.Connection.BeginGetRequestStream(this.PostStreamOpened, state);
		}
		private void PostStreamOpened(IAsyncResult result)
		{
			var state = (ConnectionState)result.AsyncState;

			Stream postStream = state.Connection.EndGetRequestStream(result);

			UTF8Encoding encoding = new UTF8Encoding();
			byte[] bytes = encoding.GetBytes(state.PostData);
			//state.Connection.ContentLength = bytes.Length;

			postStream.Write(bytes, 0, bytes.Length);
			postStream.Close();
			this.BeginGetResponse(state);

		}

		private void BeginGetResponse(ConnectionState state)
		{
			try
			{
				IAsyncResult result = (IAsyncResult)state.Connection.BeginGetResponse(
					new AsyncCallback(GetResponseHandle),
					state
				);
				ThreadPool.RegisterWaitForSingleObject(
					result.AsyncWaitHandle,
					new WaitOrTimerCallback(TimeoutCallback),
					state.Connection,
					this._ConnectionSettings.TimeOut,
					true
				);

			}
			catch (WebException e)
			{
				state.RaiseCallBack(e);
			}
			catch (Exception e)
			{
				state.RaiseCallBack(e);
			}
		}


		private static void TimeoutCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				HttpWebRequest request = state as HttpWebRequest;
				if (request != null)
				{
					request.Abort();
					allDone.Set();

				}
			}
		}


		private void GetResponseHandle(IAsyncResult result)
		{
			var state = (ConnectionState)result.AsyncState;
			try
			{
				var conn = state.Connection;
				state.Response = (HttpWebResponse)conn.EndGetResponse(result);

				Stream responseStream = state.Response.GetResponseStream();

				state.StreamResponse = responseStream;

				IAsyncResult readResult = responseStream.BeginRead(
					state.BufferRead,
					0,
					BUFFER_SIZE,
					new AsyncCallback(ReadCallBack),
					state
				);
				return;
			}
			catch (Exception e)
			{
				state.RaiseCallBack(e);
			}
		}
		private static void ReadCallBack(IAsyncResult result)
		{
			var state = (ConnectionState)result.AsyncState;
			try
			{
				var conn = state.Connection;

				Stream responseStream = state.StreamResponse;
				int read = responseStream.EndRead(result);
				if (read > 0)
				{
					state.RequestData.Append(Encoding.ASCII.GetString(state.BufferRead, 0, read));
					IAsyncResult asynchronousResult = responseStream.BeginRead(
						state.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), state);
					return;
				}
				else
				{
					state.Response.Close();
					state.RaiseCallBack();
				}

			}
			catch (WebException e)
			{
				state.RaiseCallBack(e);
			}
			catch (Exception e)
			{
				state.RaiseCallBack(e);
			}
			allDone.Set();

		}


		private HttpWebRequest CreateConnection(string path)
		{
			var url = this._CreateUriString(path);
			HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
			myReq.Accept = "application/json";
			myReq.ContentType = "application/json";
			
			if (!string.IsNullOrEmpty(this._ConnectionSettings.ProxyAddress))
			{
				var proxy = new WebProxy();
				var uri = new Uri(this._ConnectionSettings.ProxyAddress);
				var credentials = new NetworkCredential(this._ConnectionSettings.Username, this._ConnectionSettings.Password);
				proxy.Address = uri;
				proxy.Credentials = credentials;
				myReq.Proxy = proxy;
			}
			return myReq;
		}

		private string _CreateUriString(string path)
		{
			var s = this._ConnectionSettings;
			if (!path.StartsWith("/"))
				path = "/" + path;
			return string.Format("http://{0}:{1}{2}", s.Host, s.Port, path);
		}

	}
}
