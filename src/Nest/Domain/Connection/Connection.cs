using System;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;

namespace Nest
{
	internal class Connection : IConnection
	{
		public static ManualResetEvent allDone = new ManualResetEvent(false);
		const int BUFFER_SIZE = 1024;

		private IConnectionSettings _ConnectionSettings { get; set; }
		private Semaphore _ResourceLock;

		public Connection(IConnectionSettings settings)
		{
      if (settings == null)
        throw new ArgumentNullException("settings");

			this._ConnectionSettings = settings;
			this._ResourceLock = new Semaphore(settings.MaximumAsyncConnections, settings.MaximumAsyncConnections);
		}
		
		public ConnectionStatus GetSync(string path)
		{
			return this.HeaderOnlyRequest(path, "GET");
		}
		public ConnectionStatus HeadSync(string path)
		{
			return this.HeaderOnlyRequest(path, "HEAD");
		}

		public ConnectionStatus PostSync(string path, string data)
		{
			return this.BodyRequest(path, data, "POST");
		}
		public ConnectionStatus PutSync(string path, string data)
		{
			return this.BodyRequest(path, data, "PUT");
		}	
		public ConnectionStatus DeleteSync(string path)
		{
			var connection = this.CreateConnection(path, "DELETE");
			return this.DoSynchronousRequest(connection);
		}
		public ConnectionStatus DeleteSync(string path, string data)
		{
			var connection = this.CreateConnection(path, "DELETE");
			return this.DoSynchronousRequest(connection, data);
		}
	   
		public void Get(string path, Action<ConnectionStatus> callback)
		{
			Thread getThread = new Thread(() =>
			{
				this._ResourceLock.WaitOne();
				ConnectionState state = new ConnectionState()
				{
					Callback = (c) =>
					{
						this._ResourceLock.Release();
						if (callback != null)
							callback(c);

					},
					Connection = this.CreateConnection(path, "GET")
				};
				this.BeginGetResponse(state);
			});
			getThread.Start();
		}
		public void Head(string path, Action<ConnectionStatus> callback)
		{
			Thread getThread = new Thread(() =>
			{
				this._ResourceLock.WaitOne();
				ConnectionState state = new ConnectionState()
				{
					Callback = (c) =>
					{
						this._ResourceLock.Release();
						if (callback != null)
							callback(c);

					},
					Connection = this.CreateConnection(path, "HEAD")
				};
				this.BeginGetResponse(state);
			});
			getThread.Start();
		}
		public void Post(string path, string data, Action<ConnectionStatus> callback)
		{
			Thread postThread = new Thread(() =>
			{
				this._ResourceLock.WaitOne();
				this.PostDataUsingMethod("POST", path, data, (c) =>
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
			Thread putThread = new Thread(()=>
			{
				this._ResourceLock.WaitOne();
				this.PostDataUsingMethod("PUT", path, data, (c) =>
				{
					this._ResourceLock.Release();
					if (callback != null)
						callback(c);

				});	
			});
			putThread.Start();	
		}
		public void Delete(string path, string data, Action<ConnectionStatus> callback)
		{
			Thread deleteThread = new Thread(() =>
			{
				this._ResourceLock.WaitOne();
				this.PostDataUsingMethod("DELETE", path, data, (c) =>
				{
					this._ResourceLock.Release();
					if (callback != null)
						callback(c);

				});
			});
			deleteThread.Start();
		}
		public void Delete(string path, Action<ConnectionStatus> callback)
		{
			Thread deleteThread = new Thread(() =>
			{
				this._ResourceLock.WaitOne();
				ConnectionState state = new ConnectionState
				{
					Callback = (c) =>
					{
						this._ResourceLock.Release();
						if (callback != null)
							callback(c);

					},
					Connection = this.CreateConnection(path, "DELETE")
				};
				this.BeginGetResponse(state);
			});
			deleteThread.Start();
		}

		private void PostDataUsingMethod(string method, string path,string data, Action<ConnectionStatus> callback)
		{
			if (method != "PUT" && method != "POST" && method != "DELETE")
			{
				throw new ArgumentException("Valid methods that can post a body are PUT, POST, DELETE", "method");
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

			if (state.PostData != null) //TODO: look into why it is null at some points
			{
				UTF8Encoding encoding = new UTF8Encoding();
				byte[] bytes = encoding.GetBytes(state.PostData);

				postStream.Write(bytes, 0, bytes.Length);
				postStream.Close();
			}

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


		private ConnectionStatus HeaderOnlyRequest(string path, string method)
		{
			var connection = this.CreateConnection(path, method);
			return this.DoSynchronousRequest(connection);
		}

		private ConnectionStatus BodyRequest(string path, string data, string method)
		{
			var connection = this.CreateConnection(path, method);
			return this.DoSynchronousRequest(connection, data);
		}
	

		private HttpWebRequest CreateConnection(string path, string method)
		{
			var url = this._CreateUriString(path);
			if (this._ConnectionSettings.UsesPrettyResponses)
			{
				var uri = new Uri(url);
				url += ((string.IsNullOrEmpty(uri.Query)) ? "?" : "&") + "pretty=true";
			}
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

		private ConnectionStatus DoSynchronousRequest(HttpWebRequest request, string data = null)
		{
			request.Timeout = this._ConnectionSettings.TimeOut;
			Stream postStream = null;
			WebResponse response = null;
			try
			{
				if (!data.IsNullOrEmpty())
				{ 
					byte[] buffer = Encoding.UTF8.GetBytes(data);
					request.ContentLength = buffer.Length;
					postStream = request.GetRequestStream();
					postStream.Write(buffer, 0, buffer.Length);
					postStream.Close();
				}
				response = request.GetResponse();
				var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
				response.Close();
        return new ConnectionStatus(result) { Request = data };
			}
			catch (WebException e)
			{
				string result;
				var error = this.GetConnectionErrorFromWebException(e, out result);
				var status = new ConnectionStatus(error);
				status.Result = result;
        status.Request = data;
				return status;
			}
      catch (Exception e) { return new ConnectionStatus(new ConnectionError(e)) { Request = data }; }
			finally
			{
				if (postStream != null)
					postStream.Close();
				if (response != null)
					response.Close();
			}
		}
		
		private ConnectionError GetConnectionErrorFromWebException(WebException e, out string result)
		{
			result = "";
			using (var r = e.Response)
			{

				if (e.Response != null)
				{
					using (var d = e.Response.GetResponseStream())
					{ 
						result = new StreamReader(d).ReadToEnd();
					}
				}

				ConnectionError error;
				if (e.Status == WebExceptionStatus.Timeout)
				{
					error = new ConnectionError(e, result) 
					{ 
						HttpStatusCode = HttpStatusCode.InternalServerError 
					};
				}
				else
				{
					error = new ConnectionError(e, result);
				}
				return error;
			}
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
