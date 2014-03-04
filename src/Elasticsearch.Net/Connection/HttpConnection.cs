using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Exceptions;
using PUrify;

namespace Elasticsearch.Net.Connection
{
	public interface IHttpTransport
	{
		ElasticsearchResponse DoRequest(string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0);

		Task<ElasticsearchResponse> DoRequestAsync(
			string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0);
	}

	public class HttpTransport : IHttpTransport
	{
		private readonly IConnectionConfigurationValues _configurationValues;
		private readonly IConnection _connection;
		private IElasticsearchSerializer _serializer;

		public HttpTransport(IConnectionConfigurationValues configurationValues, IConnection connection, IElasticsearchSerializer serializer)
		{
			_connection = connection;
			_configurationValues = configurationValues;
			this._serializer = serializer ?? new ElasticsearchDefaultSerializer();
		}

		/// <summary>
		/// Returns either the fixed maximum set on the connection configuration settings or the number of nodes
		/// </summary>
		private int GetMaximumRetries()
		{
			return this._configurationValues.MaxRetries.GetValueOrDefault(this._configurationValues.ConnectionPool.MaxRetries);
		}

		public ElasticsearchResponse DoRequest(string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0)
		{
			if (queryString != null)
				path += queryString.ToQueryString();

			var maxRetries = this.GetMaximumRetries();
			var postData = PostData(data);
			ElasticsearchResponse response = null;
			var exceptionMessage = "Unable to perform request: '{0} {1}' on any of the nodes after retrying {2} times.".F( method, path, retried);
			var baseUri = this._configurationValues.ConnectionPool.GetNext();
			var uri = new Uri(baseUri, path);
			try
			{
				response = DoSyncRequest(method, uri, postData);
				if (response != null && response.SuccessOrKnownError)
					return response;
			}
			catch (Exception e)
			{
				if (retried < maxRetries)
					return this.DoRequest(method, path, data, null, ++retried);
				else
					throw new OutOfNodesException(exceptionMessage, e);
			}
			if (retried < maxRetries)
				return this.DoRequest(method, path, data, null, ++retried);
			
			throw new OutOfNodesException(exceptionMessage);
		}
		
		public Task<ElasticsearchResponse> DoRequestAsync(
			string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0)
		{
			if (queryString != null)
				path += queryString.ToQueryString();

			var postData = PostData(data);
			var baseUri = this._configurationValues.ConnectionPool.GetNext();
			var uri = new Uri(baseUri, path);
			
			switch (method.ToLowerInvariant())
			{
				case "post": return this._connection.Post(uri, postData);
				case "put": return this._connection.Put(uri, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this._connection.Delete(uri)
						: this._connection.Delete(uri, postData);
				case "head": return this._connection.Head(uri);
				case "get": return this._connection.Get(uri);
			}
			throw new Exception("Unknown HTTP method " + method);
		}
	
		private ElasticsearchResponse DoSyncRequest(string method, Uri uri, byte[] postData)
		{
			switch (method.ToLowerInvariant())
			{
				case "post":
					return this._connection.PostSync(uri, postData);
				case "put":
					return this._connection.PutSync(uri, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this._connection.DeleteSync(uri)
						: this._connection.DeleteSync(uri, postData);
				case "head":
					return this._connection.HeadSync(uri);
				case "get":
					return this._connection.GetSync(uri);
			}
			return null;
		}
		
		private byte[] PostData(object data)
		{
			var bytes = data as byte[];
			if (bytes != null)
				return bytes;

			var s = data as string;
			if (s != null)
				return s.Utf8Bytes();
			if (data == null) return null;
			var ss = data as IEnumerable<string>;
			if (ss != null)
				return (string.Join("\n", ss) + "\n").Utf8Bytes();
			
			var so = data as IEnumerable<object>;
			if (so == null)
				return this._serializer.Serialize(data);
			var joined = string.Join("\n", so
				.Select(soo => this._serializer.Serialize(soo, SerializationFormatting.None).Utf8String())) + "\n";
			return joined.Utf8Bytes();
		}
	}




	public class HttpConnection : IConnection
	{
		const int BUFFER_SIZE = 1024;

		protected IConnectionConfigurationValues _ConnectionSettings { get; set; }
		private Semaphore _ResourceLock;
		private readonly bool _enableTrace;

		static HttpConnection()
		{
			ServicePointManager.UseNagleAlgorithm = false;
			ServicePointManager.Expect100Continue = false;
			ServicePointManager.DefaultConnectionLimit = 10000;
		}
		public HttpConnection(IConnectionConfigurationValues settings)
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

		public ElasticsearchResponse GetSync(Uri uri)
		{
			return this.HeaderOnlyRequest(uri, "GET");
		}
		public ElasticsearchResponse HeadSync(Uri uri)
		{
			return this.HeaderOnlyRequest(uri, "HEAD");
		}

		public ElasticsearchResponse PostSync(Uri uri, byte[] data)
		{
			return this.BodyRequest(uri, data, "POST");
		}
		public ElasticsearchResponse PutSync(Uri uri, byte[] data)
		{
			return this.BodyRequest(uri, data, "PUT");
		}
		public ElasticsearchResponse DeleteSync(Uri uri)
		{
			var connection = this.CreateConnection(uri, "DELETE");
			return this.DoSynchronousRequest(connection);
		}
		public ElasticsearchResponse DeleteSync(Uri uri, byte[] data)
		{
			var connection = this.CreateConnection(uri, "DELETE");
			return this.DoSynchronousRequest(connection, data);
		}

		public Task<ElasticsearchResponse> Get(Uri uri)
		{
			var r = this.CreateConnection(uri, "GET");
			return this.DoAsyncRequest(r);
		}
		public Task<ElasticsearchResponse> Head(Uri uri)
		{
			var r = this.CreateConnection(uri, "HEAD");
			return this.DoAsyncRequest(r);
		}
		public Task<ElasticsearchResponse> Post(Uri uri, byte[] data)
		{
			var r = this.CreateConnection(uri, "POST");
			return this.DoAsyncRequest(r, data);
		}

		public Task<ElasticsearchResponse> Put(Uri uri, byte[] data)
		{
			var r = this.CreateConnection(uri, "PUT");
			return this.DoAsyncRequest(r, data);
		}

		public Task<ElasticsearchResponse> Delete(Uri uri, byte[] data)
		{
			var r = this.CreateConnection(uri, "DELETE");
			return this.DoAsyncRequest(r, data);
		}
		public Task<ElasticsearchResponse> Delete(Uri uri)
		{
			var r = this.CreateConnection(uri, "DELETE");
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

		private ElasticsearchResponse HeaderOnlyRequest(Uri uri, string method)
		{
			var connection = this.CreateConnection(uri, method);
			return this.DoSynchronousRequest(connection);
		}

		private ElasticsearchResponse BodyRequest(Uri uri, byte[] data, string method)
		{
			var connection = this.CreateConnection(uri, method);
			return this.DoSynchronousRequest(connection, data);
		}

		protected virtual HttpWebRequest CreateConnection(Uri uri, string method)
		{

			var myReq = this.CreateWebRequest(uri, method);
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
			//TODO figure out a way to cache this;

			//if (this._ConnectionSettings.UriSpecifiedBasicAuth)
			//{
				myReq.Headers["Authorization"] =
					"Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(myReq.RequestUri.UserInfo));
			//}
		}

		protected virtual HttpWebRequest CreateWebRequest(Uri uri, string method)
		{
			//TODO append global querystring
			//var url = this._CreateUriString(path);

			var myReq = (HttpWebRequest)WebRequest.Create(uri);
			//TODO move this to transport
			if (!uri.AbsolutePath.StartsWith("_cat"))
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
			var path = request.RequestUri.ToString();
			var method = request.Method;
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
				try
				{
					using (var response = (HttpWebResponse)request.GetResponse())
					using (var responseStream = response.GetResponseStream())
					using (var memoryStream = new MemoryStream())
					{
						responseStream.CopyTo(memoryStream);
						cs = ElasticsearchResponse.Create(this._ConnectionSettings, (int) response.StatusCode, method, path, data,
							memoryStream.ToArray());
						tracer.SetResult(cs);
						return cs;
					}
				}
				catch (WebException webException)
				{
					cs = ElasticsearchResponse.CreateError(this._ConnectionSettings, webException, method, path, data);
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
			var path = request.RequestUri.ToString();
			var method = request.Method;
			if (!this._ResourceLock.WaitOne(timeout))
			{
				using (var tracer = new ConnectionStatusTracer(this._ConnectionSettings.TraceEnabled))
				{
					var m = "Could not start the operation before the timeout of " + timeout +
					  "ms completed while waiting for the semaphore";
					var cs = ElasticsearchResponse.CreateError(this._ConnectionSettings, new TimeoutException(m), method, path, data); 
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
			this.Iterate(request, data, this._AsyncSteps(request, tcs, data), tcs);
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

				var path = request.RequestUri.ToString();
				var method = request.Method;

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
					var cs = ElasticsearchResponse.Create(this._ConnectionSettings, (int) response.StatusCode, method, path, data, memoryStream.ToArray());
					tcs.TrySetResult(cs);
					tracer.SetResult(cs);
					_ConnectionSettings.ConnectionStatusHandler(cs);
				}
			}
		}

		public void Iterate(HttpWebRequest request, byte[] data, IEnumerable<Task> asyncIterator, TaskCompletionSource<ElasticsearchResponse> tcs)
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
					{
						var path = request.RequestUri.ToString();
						var method = request.Method;
						var response = ElasticsearchResponse.CreateError(this._ConnectionSettings, exception, method, path, data);
						tcs.SetResult(response);
					}
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
			var uri = s.ConnectionPool.GetNext();

			if (s.QueryStringParameters != null)
			{
				var tempUri = new Uri(uri, path);
				var qs = s.QueryStringParameters.ToQueryString(tempUri.Query.IsNullOrEmpty() ? "?" : "&");
				path += qs;
			}
			uri = path.IsNullOrEmpty() ? uri : new Uri(uri, path);
			return uri.Purify();
		}

	}
}
