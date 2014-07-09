using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Providers;
using PurifyNet;

namespace Elasticsearch.Net.Connection
{
	public class HttpConnection : IConnection
	{
		const int BUFFER_SIZE = 1024;

		protected IConnectionConfigurationValues ConnectionSettings { get; set; }
		private readonly Semaphore _resourceLock;
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

			this.ConnectionSettings = settings;
			if (settings.MaximumAsyncConnections > 0)
			{
				var semaphore = Math.Max(1, settings.MaximumAsyncConnections);
				this._resourceLock = new Semaphore(semaphore, semaphore);
			}
			this._enableTrace = settings.TraceEnabled;
		}

		public virtual ElasticsearchResponse<Stream> GetSync(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			return this.HeaderOnlyRequest(uri, "GET", requestSpecificConfig);
		}
		public virtual ElasticsearchResponse<Stream> HeadSync(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			return this.HeaderOnlyRequest(uri, "HEAD", requestSpecificConfig);
		}

		public virtual ElasticsearchResponse<Stream> PostSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			return this.BodyRequest(uri, data, "POST", requestSpecificConfig);
		}
		public virtual ElasticsearchResponse<Stream> PutSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			return this.BodyRequest(uri, data, "PUT", requestSpecificConfig);
		}
		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			return this.HeaderOnlyRequest(uri, "DELETE", requestSpecificConfig);
		}
		public virtual ElasticsearchResponse<Stream> DeleteSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			return this.BodyRequest(uri, data, "DELETE", requestSpecificConfig);
		}


		private ElasticsearchResponse<Stream> HeaderOnlyRequest(Uri uri, string method, IRequestConfiguration requestSpecificConfig)
		{
			var r = this.CreateHttpWebRequest(uri, method, null, requestSpecificConfig);
			return this.DoSynchronousRequest(r, requestSpecificConfig: requestSpecificConfig);
		}

		private ElasticsearchResponse<Stream> BodyRequest(Uri uri, byte[] data, string method, IRequestConfiguration requestSpecificConfig)
		{
			var r = this.CreateHttpWebRequest(uri, method, data, requestSpecificConfig);
			return this.DoSynchronousRequest(r, data, requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Get(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			var r = this.CreateHttpWebRequest(uri, "GET", null, requestSpecificConfig);
			return this.DoAsyncRequest(r, requestSpecificConfig: requestSpecificConfig);
		}
		public virtual Task<ElasticsearchResponse<Stream>> Head(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			var r = this.CreateHttpWebRequest(uri, "HEAD", null, requestSpecificConfig);
			return this.DoAsyncRequest(r, requestSpecificConfig: requestSpecificConfig);
		}
		public virtual Task<ElasticsearchResponse<Stream>> Post(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			var r = this.CreateHttpWebRequest(uri, "POST", data, requestSpecificConfig);
			return this.DoAsyncRequest(r, data, requestSpecificConfig: requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Put(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			var r = this.CreateHttpWebRequest(uri, "PUT", data, requestSpecificConfig);
			return this.DoAsyncRequest(r, data, requestSpecificConfig: requestSpecificConfig);
		}

		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig = null)
		{
			var r = this.CreateHttpWebRequest(uri, "DELETE", data, requestSpecificConfig);
			return this.DoAsyncRequest(r, data, requestSpecificConfig: requestSpecificConfig);
		}
		public virtual Task<ElasticsearchResponse<Stream>> Delete(Uri uri, IRequestConfiguration requestSpecificConfig = null)
		{
			var r = this.CreateHttpWebRequest(uri, "DELETE", null, requestSpecificConfig);
			return this.DoAsyncRequest(r, requestSpecificConfig: requestSpecificConfig);
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


		protected virtual HttpWebRequest CreateHttpWebRequest(Uri uri, string method, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			var myReq = this.CreateWebRequest(uri, method, data, requestSpecificConfig);
			this.SetBasicAuthorizationIfNeeded(myReq);
			this.SetProxyIfNeeded(myReq);
			return myReq;
		}

		private void SetProxyIfNeeded(HttpWebRequest myReq)
		{
			if (!string.IsNullOrEmpty(this.ConnectionSettings.ProxyAddress))
			{
				var proxy = new WebProxy();
				var uri = new Uri(this.ConnectionSettings.ProxyAddress);
				var credentials = new NetworkCredential(this.ConnectionSettings.ProxyUsername, this.ConnectionSettings.ProxyPassword);
				proxy.Address = uri;
				proxy.Credentials = credentials;
				myReq.Proxy = proxy;
			}
            if(!this.ConnectionSettings.AutomaticProxyDetection)
            {
                myReq.Proxy = null;
            }
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

		protected virtual HttpWebRequest CreateWebRequest(Uri uri, string method, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			//TODO append global querystring
			//var url = this._CreateUriString(path);

			var myReq = (HttpWebRequest)WebRequest.Create(uri);

			myReq.Accept = "application/json";
			myReq.ContentType = "application/json";
			if (requestSpecificConfig != null && !string.IsNullOrWhiteSpace(requestSpecificConfig.ContentType))
			{
				myReq.Accept = requestSpecificConfig.ContentType;
				myReq.ContentType = requestSpecificConfig.ContentType;
			}
			var timeout = GetRequestTimeout(requestSpecificConfig);
			myReq.Timeout = timeout;
			myReq.ReadWriteTimeout = timeout;
			myReq.Method = method;

			//WebRequest won't send Content-Length: 0 for empty bodies
			//which goes against RFC's and might break i.e IIS when used as a proxy.
			//see: https://github.com/elasticsearch/elasticsearch-net/issues/562
			var m = method.ToLowerInvariant();
			if (m != "head" && m != "get" && (data == null || data.Length == 0))
				myReq.ContentLength = 0;

			return myReq;
		}

		protected virtual ElasticsearchResponse<Stream> DoSynchronousRequest(HttpWebRequest request, byte[] data = null, IRequestConfiguration requestSpecificConfig = null)
		{
			var path = request.RequestUri.ToString();
			var method = request.Method;

			if (data != null)
			{
				using (var r = request.GetRequestStream())
				{
					r.Write(data, 0, data.Length);
				}
			}
			try
			{
				//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
				//Either the stream or the response object needs to be closed but not both although it won't
				//throw any errors if both are closed atleast one of them has to be Closed.
				//Since we expose the stream we let closing the stream determining when to close the connection
				var response = (HttpWebResponse)request.GetResponse();
				var responseStream = response.GetResponseStream();
				return WebToElasticsearchResponse(data, responseStream, response, method, path);
			}
			catch (WebException webException)
			{
				return HandleWebException(data, webException, method, path);
			}
		}

		private ElasticsearchResponse<Stream> HandleWebException(byte[] data, WebException webException, string method, string path)
		{
			ElasticsearchResponse<Stream> cs = null;
			var httpEx = webException.Response as HttpWebResponse;
			if (httpEx != null)
			{
				cs = WebToElasticsearchResponse(data, httpEx.GetResponseStream(), httpEx, method, path);
				return cs;
			}
			cs = ElasticsearchResponse<Stream>.CreateError(this.ConnectionSettings, webException, method, path, data);
			return cs;
		}

		private ElasticsearchResponse<Stream> WebToElasticsearchResponse(byte[] data, Stream responseStream, HttpWebResponse response, string method, string path)
		{
			ElasticsearchResponse<Stream> cs = ElasticsearchResponse<Stream>.Create(this.ConnectionSettings, (int)response.StatusCode, method, path, data);
			cs.Response = responseStream;
			return cs;
		}

		protected virtual Task<ElasticsearchResponse<Stream>> DoAsyncRequest(HttpWebRequest request, byte[] data = null, IRequestConfiguration requestSpecificConfig = null)
		{
			var tcs = new TaskCompletionSource<ElasticsearchResponse<Stream>>();
			if (this.ConnectionSettings.MaximumAsyncConnections <= 0
			  || this._resourceLock == null)
				return this.CreateIterateTask(request, data, requestSpecificConfig, tcs);

			var timeout = GetRequestTimeout(requestSpecificConfig);
			var path = request.RequestUri.ToString();
			var method = request.Method;
			if (!this._resourceLock.WaitOne(timeout))
			{
				var m = "Could not start the operation before the timeout of " + timeout +
				  "ms completed while waiting for the semaphore";
				var cs = ElasticsearchResponse<Stream>.CreateError(this.ConnectionSettings, new TimeoutException(m), method, path, data);
				tcs.SetResult(cs);
				return tcs.Task;
			}
			try
			{
				return this.CreateIterateTask(request, data, requestSpecificConfig, tcs);
			}
			finally
			{
				this._resourceLock.Release();
			}
		}

		private Task<ElasticsearchResponse<Stream>> CreateIterateTask(HttpWebRequest request, byte[] data, IRequestConfiguration requestSpecificConfig, TaskCompletionSource<ElasticsearchResponse<Stream>> tcs)
		{
			this.Iterate(request, data, this._AsyncSteps(request, tcs, data, requestSpecificConfig), tcs);
			return tcs.Task;
		}

		private IEnumerable<Task> _AsyncSteps(HttpWebRequest request, TaskCompletionSource<ElasticsearchResponse<Stream>> tcs, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			var timeout = GetRequestTimeout(requestSpecificConfig);

			if (data != null)
			{
				var getRequestStream = Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null);
				ThreadPool.RegisterWaitForSingleObject((getRequestStream as IAsyncResult).AsyncWaitHandle, ThreadTimeoutCallback, request, timeout, true);
				yield return getRequestStream;

				var requestStream = getRequestStream.Result;
				try
				{
					var writeToRequestStream = Task.Factory.FromAsync(requestStream.BeginWrite, requestStream.EndWrite, data, 0, data.Length, null);
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

			var path = request.RequestUri.ToString();
			var method = request.Method;

			//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
			//Either the stream or the response object needs to be closed but not both (although it won't)
			//throw any errors if both are closed atleast one of them has to be Closed.
			//Since we expose the stream we let closing the stream determining when to close the connection
			var response = (HttpWebResponse)getResponse.Result;
			var responseStream = response.GetResponseStream();
			var cs = ElasticsearchResponse<Stream>.Create(this.ConnectionSettings, (int)response.StatusCode, method, path, data);
			cs.Response = responseStream;
			tcs.TrySetResult(cs);
		}

		private void Iterate(HttpWebRequest request, byte[] data, IEnumerable<Task> asyncIterator, TaskCompletionSource<ElasticsearchResponse<Stream>> tcs)
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
						var response = this.HandleWebException(data, exception as WebException, method, path);
						tcs.SetResult(response);
					}
					else
						tcs.TrySetException(exception);
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

		private int GetRequestTimeout(IRequestConfiguration requestConfiguration)
		{
			if (requestConfiguration != null && requestConfiguration.ConnectTimeout.HasValue)
				return requestConfiguration.RequestTimeout.Value;

			return this.ConnectionSettings.Timeout;
		}
	}
}
