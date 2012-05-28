using System;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nest
{
	internal class Connection : IConnection
	{
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
	   
		


		public Task<ConnectionStatus> Get(string path)
		{
			var r = this.CreateConnection(path, "GET");
			return this.DoAsyncRequest(r);
		}
		public Task<ConnectionStatus> Head(string path)
		{
			var r = this.CreateConnection(path, "HEAD");
			return this.DoAsyncRequest(r);
		}
		public Task<ConnectionStatus> Post(string path, string data)
		{
			var r = this.CreateConnection(path, "POST");
			return this.DoAsyncRequest(r, data);
			
		}
		public Task<ConnectionStatus> Put(string path, string data)
		{
			var r = this.CreateConnection(path, "PUT");
			return this.DoAsyncRequest(r, data);
		}
		public Task<ConnectionStatus> Delete(string path, string data)
		{
			var r = this.CreateConnection(path, "DELETE");
			return this.DoAsyncRequest(r, data);
		}
		public Task<ConnectionStatus> Delete(string path)
		{
			var r = this.CreateConnection(path, "DELETE");
			return this.DoAsyncRequest(r);
		}

		private static void ThreadTimeoutCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				HttpWebRequest request = state as HttpWebRequest;
				if (request != null)
				{
					request.Abort();
				}
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


		private Task<ConnectionStatus> DoAsyncRequest(HttpWebRequest request, string data = null)
		{
			var tcs = new TaskCompletionSource<ConnectionStatus>();
			Iterate(_AsyncSteps(request, tcs, data), tcs);
			return tcs.Task;
			
		}
		private IEnumerable<Task> _AsyncSteps(HttpWebRequest request, TaskCompletionSource<ConnectionStatus> tcs, string data = null)
		{
			var timeout = this._ConnectionSettings.TimeOut;

			if (!this._ResourceLock.WaitOne(timeout))
			{
				var m = "Could not start operation because semaphore could not be obtained before the timeout completed";
				tcs.SetResult(new ConnectionStatus(new TimeoutException(m)));
				yield break;
			}
			try
			{
				var state = new ConnectionState { Connection = request };
				var getRequestStream = Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, state);
				ThreadPool.RegisterWaitForSingleObject((getRequestStream as IAsyncResult).AsyncWaitHandle, ThreadTimeoutCallback, request, timeout, true);
				yield return getRequestStream;
				// Write the message to it
				if (data != null)
				{
					var requestStream = getRequestStream.Result;
					try
					{
						byte[] buffer = Encoding.UTF8.GetBytes(data);
						var writeToRequestStream = Task.Factory.FromAsync(requestStream.BeginWrite, requestStream.EndWrite, buffer, 0, buffer.Length, state);
						yield return writeToRequestStream;
					}
					finally
					{
						requestStream.Close();
					}
				}

				// Get the response
				var getResponse = Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
				ThreadPool.RegisterWaitForSingleObject((getResponse as IAsyncResult).AsyncWaitHandle, ThreadTimeoutCallback, request, timeout, true);
				yield return getResponse;

				// Get the response stream
				using (var response = (HttpWebResponse)getResponse.Result)
				using (var responseStream = response.GetResponseStream())
				{
					// Copy all data from the response stream
					var output = new MemoryStream();
					var buffer = new byte[BUFFER_SIZE];
					while (true)
					{
						var read = Task<int>.Factory.FromAsync(responseStream.BeginRead, responseStream.EndRead, buffer, 0, BUFFER_SIZE, null);
						yield return read;
						if (read.Result == 0) break;
						output.Write(buffer, 0, read.Result);
					}

					// Decode the data and store the result
					var result = Encoding.UTF8.GetString(output.ToArray());
					var cs = new ConnectionStatus(result) { Request = data };
					tcs.TrySetResult(cs);
				}
			}
			finally
			{
				this._ResourceLock.Release();
			}
		}
		
		public static void Iterate(IEnumerable<Task> asyncIterator, TaskCompletionSource<ConnectionStatus> tcs)
		{
			var enumerator = asyncIterator.GetEnumerator();
			Action<Task> recursiveBody = null;
			recursiveBody = completedTask =>
			{
				if (completedTask != null && completedTask.IsFaulted)
				{
					var exception = completedTask.Exception.InnerException;
					//cleanly exit from exceptions in stages
					//none of the individual steps in _AsyncSteps run in parallel for 1 request
					//as this would be impossible we can assume Aggregate Exception.InnerException
					tcs.SetResult(new ConnectionStatus(exception));
					//tcs.TrySetException();
					enumerator.Dispose();
				}
				else if (enumerator.MoveNext())
				{
					enumerator.Current.ContinueWith(recursiveBody, TaskContinuationOptions.ExecuteSynchronously);
				}
				else enumerator.Dispose();
			};
			recursiveBody(null);
		}

		private string _CreateUriString(string path)
		{
			var s = this._ConnectionSettings;
			if (!path.StartsWith("/"))
				path = "/" + path;
			return !string.IsNullOrEmpty(s.Host) ? string.Format("http://{0}:{1}{2}", s.Host, s.Port, path) : s.Uri + path;
		}

	}
}
