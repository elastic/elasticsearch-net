using System;
using System.Text;
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
		private Semaphore _ResourceLock;

		public Connection(IConnectionSettings settings)
		{
			this._ConnectionSettings = settings;
			this._ResourceLock = new Semaphore(settings.MaximumAsyncConnections, settings.MaximumAsyncConnections);
		}

		
		public ConnectionStatus GetSync(string path)
		{
			var connection = this.CreateConnection(path, "GET");
			connection.Timeout = this._ConnectionSettings.TimeOut;
			WebResponse response = null;
			try
			{
				response = connection.GetResponse();
				var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
				response.Close();
				return new ConnectionStatus(result);
			}
			catch (WebException e)
			{
				if (e.Status == WebExceptionStatus.Timeout)
					return new ConnectionStatus(new ConnectionError(e) { Type = ConnectionErrorType.Server, ExceptionMessage = "Timeout"});

				if (e.Status != WebExceptionStatus.Success
					&& e.Status != WebExceptionStatus.ProtocolError)
					return new ConnectionStatus(new ConnectionError(e) { Type = ConnectionErrorType.Server });

				return new ConnectionStatus(new ConnectionError(e));
			}
			catch (Exception e) { return new ConnectionStatus(new ConnectionError(e)); }
			finally
			{
				if (response != null)
					response.Close();
			}

		}
		public ConnectionStatus PostSync(string path, string data)
		{
			var connection = this.CreateConnection(path, "POST");
			connection.Timeout = this._ConnectionSettings.TimeOut;
			Stream postStream = null;
			WebResponse response = null;
			try
			{
				byte[] buffer = Encoding.UTF8.GetBytes(data);
				connection.ContentLength = buffer.Length;
				postStream = connection.GetRequestStream();
				postStream.Write(buffer, 0, buffer.Length);
				postStream.Close();
				response = connection.GetResponse();
				var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
				response.Close();
				return new ConnectionStatus(result);
			}
			catch (WebException e)
			{
				ConnectionError error;
				if (e.Status == WebExceptionStatus.Timeout)
				{
					error = new ConnectionError(e) { HttpStatusCode = HttpStatusCode.InternalServerError };
				}
				else
				{
					error = new ConnectionError(e);
				}
				return new ConnectionStatus(error);
			}
			catch (Exception e) { return new ConnectionStatus(new ConnectionError(e)); }
			finally
			{
				if (postStream != null)
					postStream.Close();
				if (response != null)
					response.Close();
			}
		}

        public ConnectionStatus DeleteSync(string path)
        {
            var connection = this.CreateConnection(path, "DELETE");
            connection.Timeout = this._ConnectionSettings.TimeOut;
            WebResponse response = null;
            try
            {
                response = connection.GetResponse();
                var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                return new ConnectionStatus(result);
            }
            catch (WebException e)
            {
                ConnectionError error;
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    error = new ConnectionError(e) { HttpStatusCode = HttpStatusCode.InternalServerError };
                }
                else
                {
                    error = new ConnectionError(e);
                }
                return new ConnectionStatus(error);
            }
            catch (Exception e) { return new ConnectionStatus(new ConnectionError(e)); }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        public void Delete(string path, Action<ConnectionStatus> callback)
        {
            ConnectionState state = new ConnectionState
                                        {
                                            Callback = callback,
                                            Connection = this.CreateConnection(path, "DELETE")
                                        };
            this.BeginGetResponse(state);
        }
		
		public void Get(string path, Action<ConnectionStatus> callback)
		{
			ConnectionState state = new ConnectionState()
			{
				Callback = callback,
				Connection = this.CreateConnection(path, "GET")
			};
			this.BeginGetResponse(state);
		}
		public void Post(string path, string data, Action<ConnectionStatus> callback)
		{
			Thread postThread = new Thread(
								() =>
								{
									this._ResourceLock.WaitOne();
									this._PutOrPost("POST", path, data, (c)=>
									{
										this._ResourceLock.Release();
										if (callback != null)
											callback(c);

									});
								});
			postThread.Start();
			
		}
		public void Put(string path, string data, Action<ConnectionStatus> callback)
		{
			this._PutOrPost("PUT", path, data, callback);
		}

		private void _PutOrPost(string method, string path,string data, Action<ConnectionStatus> callback)
		{
		    if (method != "PUT" && method != "POST")
		    {
		        throw new ArgumentException("Valid values contain only PUT or POST", "method");
		    }

			ConnectionState state = new ConnectionState()
			{
				Callback = callback,
				Connection = this.CreateConnection(path, method),
				PostData = data
			};

			state.Connection.BeginGetRequestStream(this.PostStreamOpened, state);
		}
		private void PostStreamOpened(IAsyncResult result)
		{
			var state = (ConnectionState)result.AsyncState;

			Stream postStream = state.Connection.EndGetRequestStream(result);

			UTF8Encoding encoding = new UTF8Encoding();
			byte[] bytes = encoding.GetBytes(state.PostData);

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
					new WaitOrTimerCallback(ThreadTimeoutCallback),
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


		private static void ThreadTimeoutCallback(object state, bool timedOut)
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
			Stream responseStream = null;
			try
			{
				var conn = state.Connection;
				state.Response = (HttpWebResponse)conn.EndGetResponse(result);

				responseStream = state.Response.GetResponseStream();
				state.StreamResponse = responseStream;

				IAsyncResult readResult = responseStream.BeginRead(
					state.BufferRead,
					0,
					BUFFER_SIZE,
					new AsyncCallback(ReadCallBack),
					state
				);
				
			}
			catch (Exception e)
			{
				state.RaiseCallBack(e);
			}
			finally
			{
			}
		}
		private static void ReadCallBack(IAsyncResult result)
		{
			var state = (ConnectionState)result.AsyncState;
			Stream responseStream = null;
			int read = 0;
			try
			{
				var conn = state.Connection;
				responseStream = state.StreamResponse;
				read = responseStream.EndRead(result);
				if (read > 0)
				{
					state.RequestData.Append(Encoding.UTF8.GetString(state.BufferRead, 0, read));
					IAsyncResult asynchronousResult = responseStream.BeginRead(
						state.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), state);
					return;
				}
				else
				{
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
			finally
			{
				if (responseStream != null && read <= 0)
					responseStream.Close();
				allDone.Set();
			}
			

		}


		private HttpWebRequest CreateConnection(string path, string method)
		{
			var url = this._CreateUriString(path);
			HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
			myReq.Accept = "application/json";
			myReq.ContentType = "application/json";
			myReq.Timeout = 1000 * 60; // 1 minute timeout.
			myReq.ReadWriteTimeout = 1000 * 60; // 1 minute timeout.
		    myReq.Method = method;
			
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
