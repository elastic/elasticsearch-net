using System;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nest
{
	public class Connection : IConnection
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

		protected virtual HttpWebRequest CreateConnection(string path, string method)
		{

			var myReq = this.CreateWebRequest(path, method);
			this.SetBasicAuthorizationIfNeeded(myReq);
			this.SetProxyIfNeeded(myReq);
			return myReq;
		}

		private void SetProxyIfNeeded(HttpWebRequest myReq)
		{
			myReq.Proxy = null;
			if (!string.IsNullOrEmpty(this._ConnectionSettings.ProxyAddress))
			{
				var proxy = new WebProxy();
				var uri = new Uri(this._ConnectionSettings.ProxyAddress);
				var credentials = new NetworkCredential(this._ConnectionSettings.ProxyUsername, this._ConnectionSettings.ProxyPassword);
				proxy.Address = uri;
				proxy.Credentials = credentials;
				myReq.Proxy = proxy;
			}
		}

		private void SetBasicAuthorizationIfNeeded(HttpWebRequest myReq)
		{
			var myUri = this._ConnectionSettings.Uri;
			if (myUri != null && !string.IsNullOrEmpty(myUri.UserInfo))
			{
				myReq.Headers["Authorization"] =
					"Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(myUri.UserInfo));
			}
		}

		private HttpWebRequest CreateWebRequest(string path, string method)
		{
			var url = this._CreateUriString(path);
			if (this._ConnectionSettings.UsesPrettyResponses)
			{
				var uri = new Uri(url);
				url += (string.IsNullOrEmpty(uri.Query) ? "?" : "&") + "pretty=true";
			}
			HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
			myReq.Accept = "application/json";
			myReq.ContentType = "application/json";

			var timeout = this._ConnectionSettings.Timeout;
			myReq.Timeout = timeout; // 1 minute timeout.
			myReq.ReadWriteTimeout = timeout; // 1 minute timeout.
			myReq.Method = method;
			return myReq;
		}

		protected virtual ConnectionStatus DoSynchronousRequest(HttpWebRequest request, string data = null)
		{
			var timeout = this._ConnectionSettings.Timeout;
			var task = this.DoAsyncRequest(request, data);
			task.Wait(timeout);
			if (task.Result == null && task.IsCanceled)
			{
				var m = "Operation did not complete before the set timeout of " + timeout + "ms";
				return new ConnectionStatus(new TimeoutException(m));
			}
			return task.Result;
		}

		protected virtual Task<ConnectionStatus> DoAsyncRequest(HttpWebRequest request, string data = null)
		{
			var timeout = this._ConnectionSettings.Timeout;

			var tcs = new TaskCompletionSource<ConnectionStatus>();
			if (!this._ResourceLock.WaitOne(timeout))
			{
				var m = "Could not start the operation before the timeout of " + timeout + "ms completed while waiting for the semaphore";
				tcs.SetResult(new ConnectionStatus(new TimeoutException(m)));
				return tcs.Task;
			}
			try
			{
				return Task.Factory.StartNew(() =>
				{
					this.Iterate(this._AsyncSteps(request, tcs, data), tcs);
					return tcs.Task.Result;
				}, TaskCreationOptions.LongRunning);
			}
			finally
			{
				this._ResourceLock.Release();
			}

		}

		private IEnumerable<Task> _AsyncSteps(HttpWebRequest request, TaskCompletionSource<ConnectionStatus> tcs, string data = null)
		{
			var timeout = this._ConnectionSettings.Timeout;

			var state = new ConnectionState { Connection = request };

			if (data != null)
			{
				var getRequestStream = Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null);
				ThreadPool.RegisterWaitForSingleObject((getRequestStream as IAsyncResult).AsyncWaitHandle, ThreadTimeoutCallback, request, timeout, true);
				yield return getRequestStream;

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
				while (responseStream != null)
				{
					var read = Task<int>.Factory.FromAsync(responseStream.BeginRead, responseStream.EndRead, buffer, 0, BUFFER_SIZE, null);
					yield return read;
					if (read.Result == 0) break;
					output.Write(buffer, 0, read.Result);
				}

				// Decode the data and store the result
				var result = Encoding.UTF8.GetString(output.ToArray());
				var cs = new ConnectionStatus(result) { Request = data, RequestUrl = request.RequestUri.ToString(), RequestMethod = request.Method };
				tcs.TrySetResult(cs);
			}
			yield break;

		}

		public void Iterate(IEnumerable<Task> asyncIterator, TaskCompletionSource<ConnectionStatus> tcs)
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
				path = "./" + path;

			return new Uri(s.Uri, path).ToString();
		}
	}
}
