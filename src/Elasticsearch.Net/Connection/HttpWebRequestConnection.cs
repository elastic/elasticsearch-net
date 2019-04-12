using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
#if DOTNETCORE
	[Obsolete("CoreFX HttpWebRequest uses HttpClient under the covers but does not reuse HttpClient instances, we'll therefore stop shipping with this class in the next major version")]
#endif
	public class HttpWebRequestConnection : IConnection
	{
		static HttpWebRequestConnection()
		{
			//Not available under mono
			if (!IsMono) HttpWebRequest.DefaultMaximumErrorResponseLength = -1;
		}

		internal static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;

		public virtual TResponse Request<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new()
		{
			int? statusCode = null;
			IEnumerable<string> warnings = null;
			Stream responseStream = null;
			Exception ex = null;
			string mimeType = null;
			try
			{
				var request = CreateHttpWebRequest(requestData);
				var data = requestData.PostData;

				if (data != null)
				{
					using (var stream = request.GetRequestStream())
					{
						if (requestData.HttpCompression)
							using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
								data.Write(zipStream, requestData.ConnectionSettings);
						else
							data.Write(stream, requestData.ConnectionSettings);
					}
				}
				requestData.MadeItToResponse = true;

				//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
				//Either the stream or the response object needs to be closed but not both although it won't
				//throw any errors if both are closed atleast one of them has to be Closed.
				//Since we expose the stream we let closing the stream determining when to close the connection
				var response = (HttpWebResponse)request.GetResponse();
				HandleResponse(response, out statusCode, out responseStream, out mimeType);

				//response.Headers.HasKeys() can return false even if response.Headers.AllKeys has values.
				if (response.SupportsHeaders && response.Headers.Count > 0 && response.Headers.AllKeys.Contains("Warning"))
					warnings = response.Headers.GetValues("Warning");
			}
			catch (WebException e)
			{
				ex = e;
				if (e.Response is HttpWebResponse response)
					HandleResponse(response, out statusCode, out responseStream, out mimeType);
			}
			responseStream = responseStream ?? Stream.Null;
			return ResponseBuilder.ToResponse<TResponse>(requestData, ex, statusCode, warnings, responseStream, mimeType);
		}

		public virtual async Task<TResponse> RequestAsync<TResponse>(RequestData requestData,
			CancellationToken cancellationToken
		)
			where TResponse : class, IElasticsearchResponse, new()
		{
			Action unregisterWaitHandle = null;
			int? statusCode = null;
			IEnumerable<string> warnings = null;
			Stream responseStream = null;
			Exception ex = null;
			string mimeType = null;
			try
			{
				var data = requestData.PostData;
				var request = CreateHttpWebRequest(requestData);
				using (cancellationToken.Register(() => request.Abort()))
				{
					if (data != null)
					{
						var apmGetRequestStreamTask =
							Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, r => request.EndGetRequestStream(r), null);
						unregisterWaitHandle = RegisterApmTaskTimeout(apmGetRequestStreamTask, request, requestData);

						using (var stream = await apmGetRequestStreamTask.ConfigureAwait(false))
						{
							if (requestData.HttpCompression)
								using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
									await data.WriteAsync(zipStream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);
							else
								await data.WriteAsync(stream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);
						}
						unregisterWaitHandle?.Invoke();
					}
					requestData.MadeItToResponse = true;
					//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
					//Either the stream or the response object needs to be closed but not both although it won't
					//throw any errors if both are closed atleast one of them has to be Closed.
					//Since we expose the stream we let closing the stream determining when to close the connection

					var apmGetResponseTask = Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, r => request.EndGetResponse(r), null);
					unregisterWaitHandle = RegisterApmTaskTimeout(apmGetResponseTask, request, requestData);

					var response = (HttpWebResponse)await apmGetResponseTask.ConfigureAwait(false);
					HandleResponse(response, out statusCode, out responseStream, out mimeType);
					if (response.SupportsHeaders && response.Headers.HasKeys() && response.Headers.AllKeys.Contains("Warning"))
						warnings = response.Headers.GetValues("Warning");
				}
			}
			catch (WebException e)
			{
				ex = e;
				if (e.Response is HttpWebResponse response)
					HandleResponse(response, out statusCode, out responseStream, out mimeType);
			}
			finally
			{
				unregisterWaitHandle?.Invoke();
			}
			responseStream = responseStream ?? Stream.Null;
			return await ResponseBuilder.ToResponseAsync<TResponse>
					(requestData, ex, statusCode, warnings, responseStream, mimeType, cancellationToken)
				.ConfigureAwait(false);
		}

		void IDisposable.Dispose() => DisposeManagedResources();

		protected virtual HttpWebRequest CreateHttpWebRequest(RequestData requestData)
		{
			var request = CreateWebRequest(requestData);
			SetBasicAuthenticationIfNeeded(request, requestData);
			SetProxyIfNeeded(request, requestData);
			SetServerCertificateValidationCallBackIfNeeded(request, requestData);
			SetClientCertificates(request, requestData);
			AlterServicePoint(request.ServicePoint, requestData);
			return request;
		}

		protected virtual void SetClientCertificates(HttpWebRequest request, RequestData requestData)
		{
			if (requestData.ClientCertificates != null)
				request.ClientCertificates.AddRange(requestData.ClientCertificates);
		}

		protected virtual void SetServerCertificateValidationCallBackIfNeeded(HttpWebRequest request, RequestData requestData)
		{
			var callback = requestData?.ConnectionSettings?.ServerCertificateValidationCallback;
#if !__MonoCS__
			//Only assign if one is defined on connection settings and a subclass has not already set one
			if (callback != null && request.ServerCertificateValidationCallback == null)
				request.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(callback);
#else
				if (callback != null)
					throw new Exception("Mono misses ServerCertificateValidationCallback on HttpWebRequest");
			#endif
		}

		protected virtual HttpWebRequest CreateWebRequest(RequestData requestData)
		{
			var request = (HttpWebRequest)WebRequest.Create(requestData.Uri);

			request.Accept = requestData.Accept;
			request.ContentType = requestData.RequestMimeType;
#if !DOTNETCORE
			// on netstandard/netcoreapp2.0 this throws argument exception
			request.MaximumResponseHeadersLength = -1;
#endif
			request.Pipelined = requestData.Pipelined;

			if (requestData.HttpCompression)
			{
				request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				request.Headers.Add("Accept-Encoding", "gzip,deflate");
				request.Headers.Add("Content-Encoding", "gzip");
			}
			if (!string.IsNullOrWhiteSpace(requestData.RunAs))
				request.Headers.Add(RequestData.RunAsSecurityHeader, requestData.RunAs);

			if (requestData.Headers != null && requestData.Headers.HasKeys())
				request.Headers.Add(requestData.Headers);

			var timeout = (int)requestData.RequestTimeout.TotalMilliseconds;
			request.Timeout = timeout;
			request.ReadWriteTimeout = timeout;

			//WebRequest won't send Content-Length: 0 for empty bodies
			//which goes against RFC's and might break i.e IIS hen used as a proxy.
			//see: https://github.com/elasticsearch/elasticsearch-net/issues/562
			var m = requestData.Method.GetStringValue();
			request.Method = m;
			if (m != "HEAD" && m != "GET" && requestData.PostData == null)
				request.ContentLength = 0;

			return request;
		}

		protected virtual void AlterServicePoint(ServicePoint requestServicePoint, RequestData requestData)
		{
			requestServicePoint.UseNagleAlgorithm = false;
			requestServicePoint.Expect100Continue = false;
			if (requestData.ConnectionSettings.ConnectionLimit > 0)
				requestServicePoint.ConnectionLimit = requestData.ConnectionSettings.ConnectionLimit;
			//looking at http://referencesource.microsoft.com/#System/net/System/Net/ServicePoint.cs
			//this method only sets internal values and wont actually cause timers and such to be reset
			//So it should be idempotent if called with the same parameters
			requestServicePoint.SetTcpKeepAlive(true, requestData.KeepAliveTime, requestData.KeepAliveInterval);
		}

		protected virtual void SetProxyIfNeeded(HttpWebRequest request, RequestData requestData)
		{
			if (!string.IsNullOrWhiteSpace(requestData.ProxyAddress))
			{
				var uri = new Uri(requestData.ProxyAddress);
				var proxy = new WebProxy(uri);
				var credentials = new NetworkCredential(requestData.ProxyUsername, requestData.ProxyPassword);
				proxy.Credentials = credentials;
				request.Proxy = proxy;
			}

			if (requestData.DisableAutomaticProxyDetection)
				request.Proxy = null;
		}

		protected virtual void SetBasicAuthenticationIfNeeded(HttpWebRequest request, RequestData requestData)
		{
			// Basic auth credentials take the following precedence (highest -> lowest):
			// 1 - Specified on the request (highest precedence)
			// 2 - Specified at the global IConnectionSettings level
			// 3 - Specified with the URI (lowest precedence)

			var userInfo = Uri.UnescapeDataString(requestData.Uri.UserInfo);

			if (requestData.BasicAuthorizationCredentials != null)
				userInfo = requestData.BasicAuthorizationCredentials.ToString();

			if (!string.IsNullOrWhiteSpace(userInfo))
				request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
		}


		/// <summary>
		/// Registers an APM async task cancellation on the threadpool
		/// </summary>
		/// <returns>An unregister action that can be used to remove the waithandle prematurely</returns>
		private static Action RegisterApmTaskTimeout(IAsyncResult result, WebRequest request, RequestData requestData)
		{
			var waitHandle = result.AsyncWaitHandle;
			var registeredWaitHandle =
				ThreadPool.RegisterWaitForSingleObject(waitHandle, TimeoutCallback, request, requestData.RequestTimeout, true);
			return () => registeredWaitHandle?.Unregister(waitHandle);
		}

		private static void TimeoutCallback(object state, bool timedOut)
		{
			if (!timedOut) return;

			(state as WebRequest)?.Abort();
		}

		private static void HandleResponse(HttpWebResponse response, out int? statusCode, out Stream responseStream, out string mimeType)
		{
			statusCode = (int)response.StatusCode;
			responseStream = response.GetResponseStream();
			mimeType = response.ContentType;
			// https://github.com/elastic/elasticsearch-net/issues/2311
			// if stream is null call dispose on response instead.
			if (responseStream == null || responseStream == Stream.Null) response.Dispose();
		}

		protected virtual void DisposeManagedResources() { }
	}
}
