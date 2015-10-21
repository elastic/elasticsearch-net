using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
			if (m != "head" && m != "get" && (requestData.Data == null))
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

		protected virtual void SetProxyIfNeeded(HttpWebRequest myReq, RequestData requestData)
		{
			if (!requestData.ProxyAddress.IsNullOrEmpty())
			{
				var proxy = new WebProxy();
				var uri = new Uri(requestData.ProxyAddress);
				var credentials = new NetworkCredential(requestData.ProxyUsername, requestData.ProxyPassword);
				proxy.Address = uri;
				proxy.Credentials = credentials;
				myReq.Proxy = proxy;
			}
			if (requestData.DisableAutomaticProxyDetection)
				myReq.Proxy = null;
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
			try
			{
				var request = this.CreateHttpWebRequest(requestData);
				var data = requestData.Data;
				if (data != null)
				{
					using (var r = request.GetRequestStream())
					{
						if (requestData.HttpCompression)
							using (var zipStream = new GZipStream(r, CompressionMode.Compress))
								requestData.Write(zipStream);
						else
							requestData.Write(r);
					}
				}
				//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
				//Either the stream or the response object needs to be closed but not both although it won't
				//throw any errors if both are closed atleast one of them has to be Closed.
				//Since we expose the stream we let closing the stream determining when to close the connection
				var response = (HttpWebResponse)request.GetResponse();
				var responseStream = response.GetResponseStream();
				var cs = requestData.CreateResponse<TReturn>((int)response.StatusCode, responseStream);
				return cs;
			}
			catch (WebException webException)
			{
				var response = (HttpWebResponse)webException.Response;
				return requestData.CreateResponse<TReturn>((int)response.StatusCode, response.GetResponseStream(), webException);
			}
			catch (Exception exception)
			{
				return requestData.CreateResponse<TReturn>(exception);
			}
		}

		public virtual async Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData) where TReturn : class
		{
			try
			{
				var request = this.CreateHttpWebRequest(requestData);
				var data = requestData.Data;
				if (data != null)
				{
					using (var r = await request.GetRequestStreamAsync())
					{
						if (requestData.HttpCompression)
							using (var zipStream = new GZipStream(r, CompressionMode.Compress))
								await requestData.WriteAsync(zipStream);
						else
							await requestData.WriteAsync(r);
					}
				}
				//http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.getresponsestream.aspx
				//Either the stream or the response object needs to be closed but not both although it won't
				//throw any errors if both are closed atleast one of them has to be Closed.
				//Since we expose the stream we let closing the stream determining when to close the connection
				var webResponse = await request.GetResponseAsync();
				var response = (HttpWebResponse)webResponse;
				var responseStream = response.GetResponseStream();
				var cs = await requestData.CreateResponseAsync<TReturn>((int)response.StatusCode, responseStream);
				return cs;
			}
			catch (WebException exception)
			{
				var response = (HttpWebResponse)exception.Response;
				return await requestData.CreateResponseAsync<TReturn>((int)response.StatusCode, response.GetResponseStream(), exception);
			}
			catch (Exception exception)
			{
				return requestData.CreateResponse<TReturn>(exception);
			}
		}
	}
}
