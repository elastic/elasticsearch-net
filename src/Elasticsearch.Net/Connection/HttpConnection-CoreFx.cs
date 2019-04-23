#if DOTNETCORE
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.DecompressionMethods;

namespace Elasticsearch.Net
{
	internal class WebProxy : IWebProxy
	{
		private readonly Uri _uri;

		public WebProxy(Uri uri) { _uri = uri; }

		public ICredentials Credentials { get; set; }

		public Uri GetProxy(Uri destination) => _uri;

		public bool IsBypassed(Uri host) => host.IsLoopback;
	}


	/// <summary> The default IConnection implementation. Uses <see cref="HttpClient"/>.</summary>
	public class HttpConnection : IConnection
	{
		private readonly object _lock = new object();

		protected readonly ConcurrentDictionary<int, HttpClient> Clients = new ConcurrentDictionary<int, HttpClient>();

		private HttpClient GetClient(RequestData requestData)
		{
			var key = GetClientKey(requestData);
			if (this.Clients.TryGetValue(key, out var client)) return client;
			lock (_lock)
			{
				client = this.Clients.GetOrAdd(key, h =>
				{
					var handler = CreateHttpClientHandler(requestData);
					var httpClient = new HttpClient(handler, false)
					{
						Timeout = requestData.RequestTimeout
					};

					httpClient.DefaultRequestHeaders.ExpectContinue = false;
					return httpClient;
				});
			}

			return client;
		}

		public virtual TResponse Request<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new()
		{
			var client = this.GetClient(requestData);
			HttpResponseMessage responseMessage = null;
			int? statusCode = null;
			IEnumerable<string> warnings = null;
			Stream responseStream = null;
			Exception ex = null;
			string mimeType = null;
			try
			{
				var requestMessage = CreateHttpRequestMessage(requestData);
				responseMessage = client.SendAsync(requestMessage).GetAwaiter().GetResult();
				requestData.MadeItToResponse = true;
				statusCode = (int) responseMessage.StatusCode;

				responseMessage.Headers.TryGetValues("Warning", out warnings);
				mimeType = responseMessage.Content.Headers.ContentType?.MediaType;

				if (responseMessage.Content != null)
					responseStream = responseMessage.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
				// https://github.com/elastic/elasticsearch-net/issues/2311
				// if stream is null call dispose on response instead.
				if (responseStream == null || responseStream == Stream.Null) responseMessage.Dispose();
			}
			catch (TaskCanceledException e)
			{
				ex = e;
			}
			catch (HttpRequestException e)
			{
				ex = e;
			}
			responseStream = responseStream ?? Stream.Null;
			var response = ResponseBuilder.ToResponse<TResponse>(requestData, ex, statusCode, warnings, responseStream, mimeType);
			//var response = builder.ToResponse();
			//explicit dispose of response not needed (as documented on MSDN) on desktop CLR
			//but we can not guarantee this is true for all HttpMessageHandler implementations
			if (typeof(TResponse) != typeof(ElasticsearchResponse<Stream>)) responseMessage?.Dispose();
			return response;
		}


		public virtual async Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new()
		{
			var client = this.GetClient(requestData);
			HttpResponseMessage responseMessage = null;
			int? statusCode = null;
			IEnumerable<string> warnings = null;
			Stream responseStream = null;
			Exception ex = null;
			string mimeType = null;
			try
			{
				var requestMessage = CreateHttpRequestMessage(requestData);
				responseMessage = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
				requestData.MadeItToResponse = true;
				mimeType = responseMessage.Content.Headers.ContentType?.MediaType;
				statusCode = (int) responseMessage.StatusCode;
				responseMessage.Headers.TryGetValues("Warning", out warnings);

				if (responseMessage.Content != null)
					responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
				// https://github.com/elastic/elasticsearch-net/issues/2311
				// if stream is null call dispose on response instead.
				if (responseStream == null || responseStream == Stream.Null) responseMessage.Dispose();
			}
			catch (TaskCanceledException e)
			{
				ex = e;
			}
			catch (HttpRequestException e)
			{
				ex = e;
			}
			responseStream = responseStream ?? Stream.Null;
			var response = await ResponseBuilder.ToResponseAsync<TResponse>
					(requestData, ex, statusCode, warnings, responseStream, mimeType, cancellationToken)
					.ConfigureAwait(false);
			//explicit dispose of response not needed (as documented on MSDN) on desktop CLR
			//but we can not guarantee this is true for all HttpMessageHandler implementations
			if (typeof(TResponse) != typeof(ElasticsearchResponse<Stream>)) responseMessage?.Dispose();
			return response;
		}

		private static readonly string MissingConnectionLimitMethodError =
			$"Your target platform does not support {nameof(ConnectionConfiguration.ConnectionLimit)}"
			+ $" please set {nameof(ConnectionConfiguration.ConnectionLimit)} to -1 on your connection configuration/settings."
			+ $" this will cause the {nameof(HttpClientHandler.MaxConnectionsPerServer)} not to be set on {nameof(HttpClientHandler)}";

		protected virtual HttpClientHandler CreateHttpClientHandler(RequestData requestData)
		{
			var handler = new HttpClientHandler
			{
				AutomaticDecompression = requestData.HttpCompression ? GZip | Deflate : None,
			};

			// same limit as desktop clr
			if (requestData.ConnectionSettings.ConnectionLimit > 0)
			{
				try
				{
					handler.MaxConnectionsPerServer = requestData.ConnectionSettings.ConnectionLimit;
				}
				catch (MissingMethodException e)
				{
					throw new Exception(MissingConnectionLimitMethodError, e);
				}
				catch (PlatformNotSupportedException e)
				{
					throw new Exception(MissingConnectionLimitMethodError, e);
				}
			}

			if (!requestData.ProxyAddress.IsNullOrEmpty())
			{
				var uri = new Uri(requestData.ProxyAddress);
				var proxy = new WebProxy(uri);
				if (!string.IsNullOrEmpty(requestData.ProxyUsername))
				{
					var credentials = new NetworkCredential(requestData.ProxyUsername, requestData.ProxyPassword);
					proxy.Credentials = credentials;
				}
				handler.Proxy = proxy;
			}
			else if (requestData.DisableAutomaticProxyDetection)
			{
				handler.UseProxy = false;
			}

			var callback = requestData?.ConnectionSettings?.ServerCertificateValidationCallback;
			if (callback != null && handler.ServerCertificateCustomValidationCallback == null)
				handler.ServerCertificateCustomValidationCallback = callback;

			if (requestData.ClientCertificates != null)
			{
				handler.ClientCertificateOptions = ClientCertificateOption.Manual;
				handler.ClientCertificates.AddRange(requestData.ClientCertificates);
			}

			return handler;
		}

		protected virtual HttpRequestMessage CreateHttpRequestMessage(RequestData requestData)
		{
			var request = this.CreateRequestMessage(requestData);
			SetBasicAuthenticationIfNeeded(request, requestData);
			return request;
		}

		protected virtual HttpRequestMessage CreateRequestMessage(RequestData requestData)
		{
			var method = ConvertHttpMethod(requestData.Method);
			var requestMessage = new HttpRequestMessage(method, requestData.Uri);

			foreach (string key in requestData.Headers)
			{
				requestMessage.Headers.TryAddWithoutValidation(key, requestData.Headers.GetValues(key));
			}
			requestMessage.Headers.Connection.Clear();
			requestMessage.Headers.ConnectionClose = false;
			requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(requestData.Accept));

			if (!requestData.RunAs.IsNullOrEmpty())
				requestMessage.Headers.Add(RequestData.RunAsSecurityHeader, requestData.RunAs);

			var data = requestData.PostData;

			if (data != null)
			{
				var stream = requestData.MemoryStreamFactory.Create();
				requestMessage.Content = new StreamContent(stream);
				requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(requestData.RequestMimeType);
				if (requestData.HttpCompression)
				{
					requestMessage.Content.Headers.Add("Content-Encoding", "gzip");
					requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
					requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
					using (var zipStream = new GZipStream(stream, CompressionMode.Compress, true))
						data.Write(zipStream, requestData.ConnectionSettings);
				}
				else
					data.Write(stream, requestData.ConnectionSettings);
				stream.Position = 0;
			}

			return requestMessage;
		}

		protected virtual void SetBasicAuthenticationIfNeeded(HttpRequestMessage requestMessage, RequestData requestData)
		{
			string userInfo = null;
			if (!requestData.Uri.UserInfo.IsNullOrEmpty())
				userInfo = Uri.UnescapeDataString(requestData.Uri.UserInfo);
			else if (requestData.BasicAuthorizationCredentials != null)
				userInfo = requestData.BasicAuthorizationCredentials.ToString();
			if (!userInfo.IsNullOrEmpty())
			{
				var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
				requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
			}
		}

		private static System.Net.Http.HttpMethod ConvertHttpMethod(HttpMethod httpMethod)
		{
			switch (httpMethod)
			{
				case HttpMethod.GET: return System.Net.Http.HttpMethod.Get;
				case HttpMethod.POST: return System.Net.Http.HttpMethod.Post;
				case HttpMethod.PUT: return System.Net.Http.HttpMethod.Put;
				case HttpMethod.DELETE: return System.Net.Http.HttpMethod.Delete;
				case HttpMethod.HEAD: return System.Net.Http.HttpMethod.Head;
				default:
					throw new ArgumentException("Invalid value for HttpMethod", nameof(httpMethod));
			}
		}

		private static int GetClientKey(RequestData requestData)
		{
			unchecked
			{
				var hashCode = requestData.RequestTimeout.GetHashCode();
				hashCode = (hashCode * 397) ^ requestData.HttpCompression.GetHashCode();
				hashCode = (hashCode * 397) ^ (requestData.ProxyAddress?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (requestData.ProxyUsername?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (requestData.ProxyPassword?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ requestData.DisableAutomaticProxyDetection.GetHashCode();
				return hashCode;
			}
		}

		void IDisposable.Dispose() => this.DisposeManagedResources();

		protected virtual void DisposeManagedResources()
		{
			foreach (var c in Clients)
				c.Value.Dispose();
		}
	}
}
#endif
