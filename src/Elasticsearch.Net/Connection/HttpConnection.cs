using System;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable VirtualMemberNeverOverriden.Global
#if !DOTNETCORE
namespace Elasticsearch.Net
{
	public class HttpConnection : IConnection
	{
		static HttpConnection()
		{
			//ServicePointManager.SetTcpKeepAlive(true, 2000, 2000);

			//WebException's GetResponse is limitted to 65kb by default.
			//Elasticsearch can be alot more chatty then that when dumping exceptions
			//On error responses, so lets up the ante.

			//Not available under mono
			if (Type.GetType("Mono.Runtime") == null)
				HttpWebRequest.DefaultMaximumErrorResponseLength = -1;
		}

		protected virtual HttpWebRequest CreateHttpWebRequest(RequestData requestData)
		{
			var request = this.CreateWebRequest(requestData);
			this.SetBasicAuthenticationIfNeeded(request, requestData);
			this.SetProxyIfNeeded(request, requestData);
			this.AlterServicePoint(request.ServicePoint, requestData);
			return request;
		}

		protected virtual HttpWebRequest CreateWebRequest(RequestData requestData)
		{
			var request = (HttpWebRequest)WebRequest.Create(requestData.Uri);

			request.Accept = requestData.ContentType;
			request.ContentType = requestData.ContentType;

			request.MaximumResponseHeadersLength = -1;

			request.Pipelined = requestData.Pipelined;

			if (requestData.HttpCompression)
			{
				request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				request.Headers.Add("Accept-Encoding", "gzip,deflate");
				request.Headers.Add("Content-Encoding", "gzip");
			}
			if (!requestData.RunAs.IsNullOrEmpty())
				request.Headers.Add("es-shield-runas-user", requestData.RunAs);

			if (requestData.Headers != null && requestData.Headers.HasKeys())
				request.Headers.Add(requestData.Headers);

			var timeout = (int)requestData.RequestTimeout.TotalMilliseconds;
			request.Timeout = timeout;
			request.ReadWriteTimeout = timeout;

			//WebRequest won't send Content-Length: 0 for empty bodies
			//which goes against RFC's and might break i.e IIS when used as a proxy.
			//see: https://github.com/elasticsearch/elasticsearch-net/issues/562
			var m = requestData.Method.GetStringValue();
			request.Method = m;
			if (m != "head" && m != "get" && (requestData.PostData == null))
				request.ContentLength = 0;

			return request;
		}

		protected virtual void AlterServicePoint(ServicePoint requestServicePoint, RequestData requestData)
		{
			requestServicePoint.UseNagleAlgorithm = false;
			requestServicePoint.Expect100Continue = false;
			requestServicePoint.ConnectionLimit = 10000;
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

				//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
				//Either the stream or the response object needs to be closed but not both although it won't
				//throw any errors if both are closed atleast one of them has to be Closed.
				//Since we expose the stream we let closing the stream determining when to close the connection
				var response = (HttpWebResponse)request.GetResponse();
				builder.StatusCode = (int)response.StatusCode;
				builder.Stream = response.GetResponseStream();
			}
			catch (WebException e)
			{
				HandleException(builder, e);
			}

			return builder.ToResponse();
		}

		public virtual async Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData) where TReturn : class
		{
			var builder = new ResponseBuilder<TReturn>(requestData);
			try
			{
				var request = this.CreateHttpWebRequest(requestData);
				var data = requestData.PostData;

				if (data != null)
				{
					using (var stream = await request.GetRequestStreamAsync().ConfigureAwait(false))
					{
						if (requestData.HttpCompression)
							using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
								await data.WriteAsync(zipStream, requestData.ConnectionSettings).ConfigureAwait(false);
						else
							await data.WriteAsync(stream, requestData.ConnectionSettings).ConfigureAwait(false);
					}
				}

				//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
				//Either the stream or the response object needs to be closed but not both although it won't
				//throw any errors if both are closed atleast one of them has to be Closed.
				//Since we expose the stream we let closing the stream determining when to close the connection
				var response = (HttpWebResponse)(await request.GetResponseAsync().ConfigureAwait(false));
				builder.StatusCode = (int)response.StatusCode;
				builder.Stream = response.GetResponseStream();
			}
			catch (WebException e)
			{
				HandleException(builder, e);
			}

			return await builder.ToResponseAsync().ConfigureAwait(false);
		}

		private void HandleException<TReturn>(ResponseBuilder<TReturn> builder, WebException exception)
			where TReturn : class
		{
			builder.Exception = exception;
			var response = exception.Response as HttpWebResponse;
			if (response != null)
			{
				builder.StatusCode = (int)response.StatusCode;
				builder.Stream = response.GetResponseStream();
			}
		}

		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources() { }
	}
}
#endif
