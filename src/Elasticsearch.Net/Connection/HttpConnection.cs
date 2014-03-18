using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Providers;
using PUrify;

namespace Elasticsearch.Net.Connection
{
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

		public virtual ElasticsearchResponse<T> GetSync<T>(Uri uri, object deserializationState = null)
		{
			return this.HeaderOnlyRequest<T>(uri, "GET", deserializationState);
		}
		public virtual ElasticsearchResponse<T> HeadSync<T>(Uri uri)
		{
			return this.HeaderOnlyRequest<T>(uri, "HEAD", null);
		}

		public virtual ElasticsearchResponse<T> PostSync<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			return this.BodyRequest<T>(uri, data, "POST", deserializationState);
		}
		public virtual ElasticsearchResponse<T> PutSync<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			return this.BodyRequest<T>(uri, data, "PUT", deserializationState);
		}
		public virtual ElasticsearchResponse<T> DeleteSync<T>(Uri uri, object deserializationState = null)
		{
			return this.HeaderOnlyRequest<T>(uri, "DELETE", deserializationState);
		}
		public virtual ElasticsearchResponse<T> DeleteSync<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			return this.BodyRequest<T>(uri, data, "DELETE", deserializationState);
		}

		
		public virtual bool Ping(Uri uri)
		{
			var request = this.CreateHttpWebRequest(uri, "HEAD");
			request.Timeout = this._ConnectionSettings.PingTimeout.GetValueOrDefault(50);
			request.ReadWriteTimeout = this._ConnectionSettings.PingTimeout.GetValueOrDefault(50);
			using (var response = (HttpWebResponse)request.GetResponse())
			{
				return response.StatusCode == HttpStatusCode.OK;
			}
		}

		public virtual IList<Uri> Sniff(Uri uri)
		{
			uri = new Uri(uri, "_nodes/_all/clear?timeout=" + this._ConnectionSettings.PingTimeout.GetValueOrDefault(50));
			var request = this.CreateHttpWebRequest(uri, "GET");
			request.Timeout = this._ConnectionSettings.Timeout;
			request.ReadWriteTimeout = this._ConnectionSettings.Timeout;
			using (var response = (HttpWebResponse)request.GetResponse())
			using (var responseStream = response.GetResponseStream())
			{
				if (response.StatusCode != HttpStatusCode.OK)
					return new List<Uri>();
				var cs = ElasticsearchResponse<object>.Create(this._ConnectionSettings, (int)response.StatusCode, "GET", uri.AbsolutePath, null);
				return Sniffer.FromStream(cs, responseStream, this._ConnectionSettings.Serializer);
			}
		}

		public virtual Task<ElasticsearchResponse<T>> Get<T>(Uri uri, object deserializationState = null)
		{
			var r = this.CreateHttpWebRequest(uri, "GET");
			return this.DoAsyncRequest<T>(r);
		}
		public virtual Task<ElasticsearchResponse<T>> Head<T>(Uri uri)
		{
			var r = this.CreateHttpWebRequest(uri, "HEAD");
			return this.DoAsyncRequest<T>(r);
		}
		public virtual Task<ElasticsearchResponse<T>> Post<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			var r = this.CreateHttpWebRequest(uri, "POST");
			return this.DoAsyncRequest<T>(r, data);
		}

		public virtual Task<ElasticsearchResponse<T>> Put<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			var r = this.CreateHttpWebRequest(uri, "PUT");
			return this.DoAsyncRequest<T>(r, data);
		}

		public virtual Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, byte[] data, object deserializationState = null)
		{
			var r = this.CreateHttpWebRequest(uri, "DELETE");
			return this.DoAsyncRequest<T>(r, data);
		}
		public virtual Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, object deserializationState = null)
		{
			var r = this.CreateHttpWebRequest(uri, "DELETE");
			return this.DoAsyncRequest<T>(r);
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

		private ElasticsearchResponse<T> HeaderOnlyRequest<T>(Uri uri, string method, object deserializationState)
		{
			var r = this.CreateHttpWebRequest(uri, method);
			return this.DoSynchronousRequest<T>(r, deserializationState: deserializationState);
		}

		private ElasticsearchResponse<T> BodyRequest<T>(Uri uri, byte[] data, string method, object deserializationState)
		{
			var r = this.CreateHttpWebRequest(uri, method);
			return this.DoSynchronousRequest<T>(r, data, deserializationState);
		}

		protected virtual HttpWebRequest CreateHttpWebRequest(Uri uri, string method)
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

		protected virtual ElasticsearchResponse<T> DoSynchronousRequest<T>(HttpWebRequest request, byte[] data = null, object deserializationState = null)
		{
			var path = request.RequestUri.ToString();
			var method = request.Method;
			using (var tracer = new ElasticsearchResponseTracer<T>(this._ConnectionSettings.TraceEnabled))
			{
				ElasticsearchResponse<T> cs = null;
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
						Stream s = responseStream;
						if(_ConnectionSettings.UsesPrettyResponses) //TODO different setting
						{
							responseStream.CopyTo(memoryStream);
							//use memory stream for serialization instead
							//our own serializers have special handling for memorystream
							//that will prevent double reads
							s = memoryStream;
						}

						cs = ElasticsearchResponse<T>.Create(this._ConnectionSettings, (int) response.StatusCode, method, path, data);
						var result = this._ConnectionSettings.Serializer.Deserialize<T>(cs, s, deserializationState);
						cs.Response = result;
						cs.ResponseRaw = memoryStream.ToArray();
						tracer.SetResult(cs);
						return cs;
					}
				}
				catch (WebException webException)
				{
					cs = ElasticsearchResponse<T>.CreateError(this._ConnectionSettings, webException, method, path, data);
					tracer.SetResult(cs);
					_ConnectionSettings.ConnectionStatusHandler(cs);
					return cs;
				}
			}

		}

		protected virtual Task<ElasticsearchResponse<T>> DoAsyncRequest<T>(HttpWebRequest request, byte[] data = null, object deserializationState = null)
		{
			var tcs = new TaskCompletionSource<ElasticsearchResponse<T>>();
			if (this._ConnectionSettings.MaximumAsyncConnections <= 0
			  || this._ResourceLock == null)
				return this.CreateIterateTask<T>(request, data, deserializationState, tcs);

			var timeout = this._ConnectionSettings.Timeout;
			var path = request.RequestUri.ToString();
			var method = request.Method;
			if (!this._ResourceLock.WaitOne(timeout))
			{
				using (var tracer = new ElasticsearchResponseTracer<T>(this._ConnectionSettings.TraceEnabled))
				{
					var m = "Could not start the operation before the timeout of " + timeout +
					  "ms completed while waiting for the semaphore";
					var cs = ElasticsearchResponse<T>.CreateError(this._ConnectionSettings, new TimeoutException(m), method, path, data); 
					tcs.SetResult(cs);
					tracer.SetResult(cs);
					_ConnectionSettings.ConnectionStatusHandler(cs);
					return tcs.Task;
				}
			}
			try
			{
				return this.CreateIterateTask<T>(request, data, deserializationState, tcs);
			}
			finally
			{
				this._ResourceLock.Release();
			}
		}

		private Task<ElasticsearchResponse<T>> CreateIterateTask<T>(HttpWebRequest request, byte[] data, object deserializationState, TaskCompletionSource<ElasticsearchResponse<T>> tcs)
		{
			this.Iterate<T>(request, data, this._AsyncSteps<T>(request, tcs, data, deserializationState), tcs);
			return tcs.Task;
		}

		private IEnumerable<Task> _AsyncSteps<T>(HttpWebRequest request, TaskCompletionSource<ElasticsearchResponse<T>> tcs, byte[] data, object deserializationState)
		{
			using (var tracer = new ElasticsearchResponseTracer<T>(this._ConnectionSettings.TraceEnabled))
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
					Stream s = responseStream;
					if (_ConnectionSettings.UsesPrettyResponses)
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
						s = memoryStream;
					}
					var cs = ElasticsearchResponse<T>.Create(this._ConnectionSettings, (int) response.StatusCode, method, path, data);
					var t = this._ConnectionSettings.Serializer.DeserializeAsync<T>(cs, s, deserializationState);
					yield return t;
					cs.Response = t.Result;
					cs.ResponseRaw = memoryStream.ToArray();
					tcs.TrySetResult(cs);
					tracer.SetResult(cs);
					_ConnectionSettings.ConnectionStatusHandler(cs);
				}
			}
		}

		public void Iterate<T>(HttpWebRequest request, byte[] data, IEnumerable<Task> asyncIterator, TaskCompletionSource<ElasticsearchResponse<T>> tcs)
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
						var response = ElasticsearchResponse<T>.CreateError(this._ConnectionSettings, exception, method, path, data);
						tcs.SetResult(response);
					}
					else
						tcs.TrySetException(exception);
					enumerator.Dispose();
				}
				else if (enumerator.MoveNext())
				{
					enumerator.Current.ContinueWith(recursiveBody);
				}
				else enumerator.Dispose();
			};
			recursiveBody(null);
		}

		

	}
}
