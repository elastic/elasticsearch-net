using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PUrify;

namespace Elasticsearch.Net.Connection
{
	public class HttpConnection : IConnection
	{
		const int BUFFER_SIZE = 1024;

		protected IConnectionSettings2 _ConnectionSettings { get; set; }
		private Semaphore _ResourceLock;
		private readonly bool _enableTrace;

		static HttpConnection()
		{
			ServicePointManager.UseNagleAlgorithm = false;
			ServicePointManager.Expect100Continue = false;
			ServicePointManager.DefaultConnectionLimit = 10000;
		}
		public HttpConnection(IConnectionSettings2 settings)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this._ConnectionSettings = settings;
			if (settings.MaximumAsyncConnections > 0)
			{
				var semaphore = Math.Max(1, settings.MaximumAsyncConnections);
				this._ResourceLock = new Semaphore(semaphore, semaphore);
			}
			this._enableTrace = settings.TraceEnabled;
		}

		public ElasticsearchResponse GetSync(string path)
		{
			return this.HeaderOnlyRequest(path, "GET");
		}
		public ElasticsearchResponse HeadSync(string path)
		{
			return this.HeaderOnlyRequest(path, "HEAD");
		}

		public ElasticsearchResponse PostSync(string path, byte[] data)
		{
			return this.BodyRequest(path, data, "POST");
		}
		public ElasticsearchResponse PutSync(string path, byte[] data)
		{
			return this.BodyRequest(path, data, "PUT");
		}
		public ElasticsearchResponse DeleteSync(string path)
		{
			var connection = this.CreateConnection(path, "DELETE");
			return this.DoSynchronousRequest(connection);
		}
		public ElasticsearchResponse DeleteSync(string path, byte[] data)
		{
			var connection = this.CreateConnection(path, "DELETE");
			return this.DoSynchronousRequest(connection, data);
		}

		public Task<ElasticsearchResponse> Get(string path)
		{
			var r = this.CreateConnection(path, "GET");
			return this.DoAsyncRequest(r);
		}
		public Task<ElasticsearchResponse> Head(string path)
		{
			var r = this.CreateConnection(path, "HEAD");
			return this.DoAsyncRequest(r);
		}
		public Task<ElasticsearchResponse> Post(string path, byte[] data)
		{
			var r = this.CreateConnection(path, "POST");
			return this.DoAsyncRequest(r, data);
		}

		public Task<ElasticsearchResponse> Put(string path, byte[] data)
		{
			var r = this.CreateConnection(path, "PUT");
			return this.DoAsyncRequest(r, data);
		}

		public Task<ElasticsearchResponse> Delete(string path, byte[] data)
		{
			var r = this.CreateConnection(path, "DELETE");
			return this.DoAsyncRequest(r, data);
		}
		public Task<ElasticsearchResponse> Delete(string path)
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

		private ElasticsearchResponse HeaderOnlyRequest(string path, string method)
		{
			var connection = this.CreateConnection(path, method);
			return this.DoSynchronousRequest(connection);
		}

		private ElasticsearchResponse BodyRequest(string path, byte[] data, string method)
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
			if (!string.IsNullOrEmpty(this._ConnectionSettings.ProxyAddress))
			{
				var proxy = new WebProxy();
				var uri = new Uri(this._ConnectionSettings.ProxyAddress);
				var credentials = new NetworkCredential(this._ConnectionSettings.ProxyUsername, this._ConnectionSettings.ProxyPassword);
				proxy.Address = uri;
				proxy.Credentials = credentials;
				myReq.Proxy = proxy;
			}
			//myReq.Proxy = null;
		}

		private void SetBasicAuthorizationIfNeeded(HttpWebRequest myReq)
		{
			if (this._ConnectionSettings.UriSpecifiedBasicAuth)
			{
				myReq.Headers["Authorization"] =
				  "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(this._ConnectionSettings.Uri.UserInfo));
			}
		}

		protected virtual HttpWebRequest CreateWebRequest(string path, string method)
		{
			var url = this._CreateUriString(path);

			var myReq = (HttpWebRequest)WebRequest.Create(url);
			if (!path.StartsWith("_cat"))
			{
				myReq.Accept = "application/json";
				myReq.ContentType = "application/json";
			}
			var timeout = this._ConnectionSettings.Timeout;
			myReq.Timeout = timeout; // 1 minute timeout.
			myReq.ReadWriteTimeout = timeout; // 1 minute timeout.
			myReq.Method = method;
			return myReq;
		}

		protected virtual ElasticsearchResponse DoSynchronousRequest(HttpWebRequest request, byte[] data = null)
		{
			using (var tracer = new ConnectionStatusTracer(this._ConnectionSettings.TraceEnabled))
			{
				ElasticsearchResponse cs = null;
				if (data != null)
				{
					using (var r = request.GetRequestStream())
					{
						r.Write(data, 0, data.Length);
					}
				}
				var requestData = data.Utf8String();
				try
				{
					using (var response = (HttpWebResponse)request.GetResponse())
					using (var responseStream = response.GetResponseStream())
					using (var memoryStream = new MemoryStream())
					{
						responseStream.CopyTo(memoryStream);
						cs = new ElasticsearchResponse(this._ConnectionSettings, memoryStream.ToArray())
						{
							Request = requestData,
							RequestUrl = request.RequestUri.ToString(),
							RequestMethod = request.Method
						};
						tracer.SetResult(cs);
						return cs;
					}
				}
				catch (WebException webException)
				{
					cs = new ElasticsearchResponse(this._ConnectionSettings, webException)
					{
						Request = requestData,
						RequestUrl = request.RequestUri.ToString(),
						RequestMethod = request.Method
					};
					tracer.SetResult(cs);
					_ConnectionSettings.ConnectionStatusHandler(cs);
					return cs;
				}
			}

		}

		protected virtual Task<ElasticsearchResponse> DoAsyncRequest(HttpWebRequest request, byte[] data = null)
		{
			var tcs = new TaskCompletionSource<ElasticsearchResponse>();
			if (this._ConnectionSettings.MaximumAsyncConnections <= 0
			  || this._ResourceLock == null)
				return this.CreateIterateTask(request, data, tcs);

			var timeout = this._ConnectionSettings.Timeout;
			if (!this._ResourceLock.WaitOne(timeout))
			{
				using (var tracer = new ConnectionStatusTracer(this._ConnectionSettings.TraceEnabled))
				{
					var m = "Could not start the operation before the timeout of " + timeout +
					  "ms completed while waiting for the semaphore";
					var cs = new ElasticsearchResponse(this._ConnectionSettings, new TimeoutException(m));
					tcs.SetResult(cs);
					tracer.SetResult(cs);
					_ConnectionSettings.ConnectionStatusHandler(cs);
					return tcs.Task;
				}
			}
			try
			{
				return this.CreateIterateTask(request, data, tcs);
			}
			finally
			{
				this._ResourceLock.Release();
			}
		}

		private Task<ElasticsearchResponse> CreateIterateTask(HttpWebRequest request, byte[] data, TaskCompletionSource<ElasticsearchResponse> tcs)
		{
			this.Iterate(this._AsyncSteps(request, tcs, data), tcs);
			return tcs.Task;
		}

		private IEnumerable<Task> _AsyncSteps(HttpWebRequest request, TaskCompletionSource<ElasticsearchResponse> tcs, byte[] data = null)
		{
			using (var tracer = new ConnectionStatusTracer(this._ConnectionSettings.TraceEnabled))
			{
				var timeout = this._ConnectionSettings.Timeout;

				var state = new ConnectionState { Connection = request };

				if (data != null)
				{
					var getRequestStream = Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null);
					//ThreadPool.RegisterWaitForSingleObject((getRequestStream as IAsyncResult).AsyncWaitHandle, ThreadTimeoutCallback, request, timeout, true);
					yield return getRequestStream;

					var requestStream = getRequestStream.Result;
					try
					{
						var writeToRequestStream = Task.Factory.FromAsync(requestStream.BeginWrite, requestStream.EndWrite, data, 0, data.Length, state);
						yield return writeToRequestStream;
					}
					finally
					{
						requestStream.Close();
					}
				}

				// Get the response
				var getResponse = Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
				//ThreadPool.RegisterWaitForSingleObject((getResponse as IAsyncResult).AsyncWaitHandle, ThreadTimeoutCallback, request, timeout, true);
				yield return getResponse;

				// Get the response stream
				using (var response = (HttpWebResponse)getResponse.Result)
				using (var responseStream = response.GetResponseStream())
				using (var memoryStream = new MemoryStream())
				{
					// Copy all data from the response stream
					var buffer = new byte[BUFFER_SIZE];
					while (responseStream != null)
					{
						var read = Task<int>.Factory.FromAsync(responseStream.BeginRead, responseStream.EndRead, buffer, 0, BUFFER_SIZE, null);
						yield return read;
						if (read.Result == 0) break;
						memoryStream.Write(buffer, 0, read.Result);
					}

					// Decode the data and store the result
					var cs = new ElasticsearchResponse(this._ConnectionSettings, memoryStream.ToArray())
					{
						Request = data.Utf8String(),
						RequestUrl = request.RequestUri.ToString(),
						RequestMethod = request.Method
					};
					tcs.TrySetResult(cs);
					tracer.SetResult(cs);
					_ConnectionSettings.ConnectionStatusHandler(cs);
				}
			}
		}

		public void Iterate(IEnumerable<Task> asyncIterator, TaskCompletionSource<ElasticsearchResponse> tcs)
		{
			var enumerator = asyncIterator.GetEnumerator();
			Action<Task> recursiveBody = null;
			recursiveBody = completedTask =>
			{
				if (completedTask != null && completedTask.IsFaulted)
				{
					//none of the individual steps in _AsyncSteps run in parallel for 1 request
					//as this would be impossible we can assume Aggregate Exception.InnerException
					var exception = completedTask.Exception.InnerException;

					//cleanly exit from exceptions in stages if the exception is a webexception
					if (exception is WebException)
						tcs.SetResult(new ElasticsearchResponse(this._ConnectionSettings, exception));
					else
						tcs.TrySetException(exception);
					//					enumerator.Dispose();
				}
				else if (enumerator.MoveNext())
				{
					//enumerator.Current.ContinueWith(recursiveBody, TaskContinuationOptions.ExecuteSynchronously);
					enumerator.Current.ContinueWith(recursiveBody);
				}
				else enumerator.Dispose();
			};
			recursiveBody(null);
		}

		private Uri _CreateUriString(string path)
		{
			var s = this._ConnectionSettings;


			if (s.QueryStringParameters != null)
			{
				var tempUri = new Uri(s.Uri, path);
				var qs = s.QueryStringParameters.ToQueryString(tempUri.Query.IsNullOrEmpty() ? "?" : "&");
				path += qs;
			}
			Uri uri = path.IsNullOrEmpty() ? s.Uri : new Uri(s.Uri, path);
			return uri.Purify();
		}

	}
}
