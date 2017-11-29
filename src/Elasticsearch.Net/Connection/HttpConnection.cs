using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable VirtualMemberNeverOverriden.Global
#if !DOTNETCORE
namespace Elasticsearch.Net
{
	public class HttpConnection : IConnection
	{
		internal static bool IsMono { get; } = Type.GetType("Mono.Runtime") != null;

		static HttpConnection()
		{
			//Not available under mono
			if (!IsMono)
				HttpWebRequest.DefaultMaximumErrorResponseLength = -1;
		}

		protected virtual HttpWebRequest CreateHttpWebRequest(RequestData requestData)
		{
			var request = this.CreateWebRequest(requestData);
			this.SetBasicAuthenticationIfNeeded(request, requestData);
			this.SetProxyIfNeeded(request, requestData);
			this.SetServerCertificateValidationCallBackIfNeeded(request, requestData);
			this.SetClientCertificates(request, requestData);
			this.AlterServicePoint(request.ServicePoint, requestData);
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
			var request = (HttpWebRequest) WebRequest.Create(requestData.Uri);

			request.Accept = requestData.Accept;
			request.ContentType = requestData.ContentType;
			request.MaximumResponseHeadersLength = -1;
			request.AllowWriteStreamBuffering = false;
			request.Pipelined = requestData.Pipelined;

			if (requestData.HttpCompression)
			{
				request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				request.Headers.Add("Content-Encoding", "gzip");
			}
			if (!requestData.RunAs.IsNullOrEmpty())
				request.Headers.Add(RequestData.RunAsSecurityHeader, requestData.RunAs);

			if (requestData.Headers != null && requestData.Headers.HasKeys())
				request.Headers.Add(requestData.Headers);

			var timeout = (int) requestData.RequestTimeout.TotalMilliseconds;
			request.Timeout = timeout;
			request.ReadWriteTimeout = timeout;

			//WebRequest won't send Content-Length: 0 for empty bodies
			//which goes against RFC's and might break i.e IIS when used as a proxy.
			//see: https://github.com/elasticsearch/elasticsearch-net/issues/562.
			//Transfer-Encoding: chunked must be set for POST and PUT where there is a body.
			var method = requestData.Method.GetStringValue();
			request.Method = method;
			if (method != "HEAD" && method != "GET")
			{
				if (requestData.PostData == null)
					request.ContentLength = 0;
				else
					request.SendChunked = true;
			}

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
			if (!requestData.ProxyAddress.IsNullOrEmpty())
			{
				var proxy = new WebProxy();
				var uri = new Uri(requestData.ProxyAddress);
				var credentials = new NetworkCredential(requestData.ProxyUsername, requestData.ProxyPassword);
				proxy.Address = uri;
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

			if (!userInfo.IsNullOrEmpty())
				request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
		}

		public virtual ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData) where TReturn : class
		{
			var builder = new ResponseBuilder<TReturn>(requestData);
			try
			{
				var request = this.CreateHttpWebRequest(requestData);
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
				var response = (HttpWebResponse) request.GetResponse();
				builder.StatusCode = (int) response.StatusCode;
				builder.Stream = response.GetResponseStream();

				if (response.SupportsHeaders && response.Headers.HasKeys() && response.Headers.AllKeys.Contains("Warning"))
					builder.DeprecationWarnings = response.Headers.GetValues("Warning");
				// https://github.com/elastic/elasticsearch-net/issues/2311
				// if stream is null call dispose on response instead.
				if (builder.Stream == null || builder.Stream == Stream.Null) response.Dispose();
			}
			catch (WebException e)
			{
				HandleException(builder, e);
			}

			return builder.ToResponse();
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

		public virtual async Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData,
			CancellationToken cancellationToken) where TReturn : class
		{
			var builder = new ResponseBuilder<TReturn>(requestData, cancellationToken);
			Action unregisterWaitHandle = null;
			try
			{
				var data = requestData.PostData;
				var request = this.CreateHttpWebRequest(requestData);
				using (cancellationToken.Register(() => request.Abort()))
				{
					if (data != null)
					{
						var apmGetRequestStreamTask = Task.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, null);
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

					var apmGetResponseTask = Task.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);
					unregisterWaitHandle = RegisterApmTaskTimeout(apmGetResponseTask, request, requestData);

					var response = (HttpWebResponse) (await apmGetResponseTask.ConfigureAwait(false));
					builder.StatusCode = (int) response.StatusCode;
					builder.Stream = response.GetResponseStream();
					if (response.SupportsHeaders && response.Headers.HasKeys() && response.Headers.AllKeys.Contains("Warning"))
						builder.DeprecationWarnings = response.Headers.GetValues("Warning");
					// https://github.com/elastic/elasticsearch-net/issues/2311
					// if stream is null call dispose on response instead.
					if (builder.Stream == null || builder.Stream == Stream.Null) response.Dispose();
				}
			}
			catch (WebException e)
			{
				HandleException(builder, e);
			}
			finally
			{
				unregisterWaitHandle?.Invoke();
			}
			return await builder.ToResponseAsync().ConfigureAwait(false);
		}

		private static void HandleException<TReturn>(ResponseBuilder<TReturn> builder, WebException exception)
			where TReturn : class
		{
			builder.Exception = exception;
			var response = exception.Response as HttpWebResponse;
			if (response == null) return;
			builder.StatusCode = (int) response.StatusCode;
			builder.Stream = response.GetResponseStream();
			// https://github.com/elastic/elasticsearch-net/issues/2311
			// if stream is null call dispose on response instead.
			if (builder.Stream == null || builder.Stream == Stream.Null) response.Dispose();
		}

		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
#endif
