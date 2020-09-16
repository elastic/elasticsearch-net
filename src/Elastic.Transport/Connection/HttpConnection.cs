// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

#if DOTNETCORE
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Diagnostics;
using Elasticsearch.Net.Extensions;
using static System.Net.DecompressionMethods;

namespace Elasticsearch.Net
{
	internal class WebProxy : IWebProxy
	{
		private readonly Uri _uri;

		public WebProxy(Uri uri) => _uri = uri;

		public ICredentials Credentials { get; set; }

		public Uri GetProxy(Uri destination) => _uri;

		public bool IsBypassed(Uri host) => host.IsLoopback;
	}


	/// <summary> The default IConnection implementation. Uses <see cref="HttpClient" />.</summary>
	public class HttpConnection : IConnection
	{
		private static DiagnosticSource DiagnosticSource { get; } = new DiagnosticListener(DiagnosticSources.HttpConnection.SourceName);

		private static readonly string MissingConnectionLimitMethodError =
			$"Your target platform does not support {nameof(ConnectionConfiguration.ConnectionLimit)}"
			+ $" please set {nameof(ConnectionConfiguration.ConnectionLimit)} to -1 on your connection configuration/settings."
			+ $" this will cause the {nameof(HttpClientHandler.MaxConnectionsPerServer)} not to be set on {nameof(HttpClientHandler)}";

		private readonly object _lock = new object();
		private RequestDataHttpClientFactory HttpClientFactory { get; }

		public int InUseHandlers => HttpClientFactory.InUseHandlers;
		public int RemovedHandlers => HttpClientFactory.RemovedHandlers;

		public HttpConnection() => HttpClientFactory = new RequestDataHttpClientFactory(r => CreateHttpClientHandler(r));

		public virtual TResponse Request<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new()
		{
			var client = GetClient(requestData);
			HttpResponseMessage responseMessage = null;
			int? statusCode = null;
			IEnumerable<string> warnings = null;
			Stream responseStream = null;
			Exception ex = null;
			string mimeType = null;
			IDisposable receive = DiagnosticSources.SingletonDisposable;
			ReadOnlyDictionary<TcpState, int> tcpStats = null;
			ReadOnlyDictionary<string, ThreadPoolStatistics> threadPoolStats = null;

			try
			{
				var requestMessage = CreateHttpRequestMessage(requestData);

				if (requestData.PostData != null)
					SetContent(requestMessage, requestData);

				using(requestMessage?.Content ?? (IDisposable)Stream.Null)
				using (var d = DiagnosticSource.Diagnose<RequestData, int?>(DiagnosticSources.HttpConnection.SendAndReceiveHeaders, requestData))
				{
					if (requestData.TcpStats)
						tcpStats = TcpStats.GetStates();

					if (requestData.ThreadPoolStats)
						threadPoolStats = ThreadPoolStats.GetStats();

					responseMessage = client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();
					statusCode = (int)responseMessage.StatusCode;
					d.EndState = statusCode;
				}

				requestData.MadeItToResponse = true;
				responseMessage.Headers.TryGetValues("Warning", out warnings);
				mimeType = responseMessage.Content.Headers.ContentType?.MediaType;

				if (responseMessage.Content != null)
				{
					receive = DiagnosticSource.Diagnose(DiagnosticSources.HttpConnection.ReceiveBody, requestData, statusCode);
					responseStream = responseMessage.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
				}
			}
			catch (TaskCanceledException e)
			{
				ex = e;
			}
			catch (HttpRequestException e)
			{
				ex = e;
			}
			using(receive)
			using (responseStream ??= Stream.Null)
			{
				var response = ResponseBuilder.ToResponse<TResponse>(requestData, ex, statusCode, warnings, responseStream, mimeType);

				// set TCP and threadpool stats on the response here so that in the event the request fails after the point of
				// gathering stats, they are still exposed on the call details. Ideally these would be set inside ResponseBuilder.ToResponse,
				// but doing so would be a breaking change in 7.x
				response.ApiCall.TcpStats = tcpStats;
				response.ApiCall.ThreadPoolStats = threadPoolStats;
				return response;
			}
		}

		public virtual async Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new()
		{
			var client = GetClient(requestData);
			HttpResponseMessage responseMessage = null;
			int? statusCode = null;
			IEnumerable<string> warnings = null;
			Stream responseStream = null;
			Exception ex = null;
			string mimeType = null;
			IDisposable receive = DiagnosticSources.SingletonDisposable;
			ReadOnlyDictionary<TcpState, int> tcpStats = null;
			ReadOnlyDictionary<string, ThreadPoolStatistics> threadPoolStats = null;

			try
			{
				var requestMessage = CreateHttpRequestMessage(requestData);

				if (requestData.PostData != null)
					await SetContentAsync(requestMessage, requestData, cancellationToken).ConfigureAwait(false);

				using(requestMessage?.Content ?? (IDisposable)Stream.Null)
				using (var d = DiagnosticSource.Diagnose<RequestData, int?>(DiagnosticSources.HttpConnection.SendAndReceiveHeaders, requestData))
				{
					if (requestData.TcpStats)
						tcpStats = TcpStats.GetStates();

					if (requestData.ThreadPoolStats)
						threadPoolStats = ThreadPoolStats.GetStats();

					responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
					statusCode = (int)responseMessage.StatusCode;
					d.EndState = statusCode;
				}

				requestData.MadeItToResponse = true;
				mimeType = responseMessage.Content.Headers.ContentType?.MediaType;
				responseMessage.Headers.TryGetValues("Warning", out warnings);

				if (responseMessage.Content != null)
				{
					receive = DiagnosticSource.Diagnose(DiagnosticSources.HttpConnection.ReceiveBody, requestData, statusCode);
					responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
				}
			}
			catch (TaskCanceledException e)
			{
				ex = e;
			}
			catch (HttpRequestException e)
			{
				ex = e;
			}
			using (receive)
			using (responseStream = responseStream ?? Stream.Null)
			{
				var response = await ResponseBuilder.ToResponseAsync<TResponse>
						(requestData, ex, statusCode, warnings, responseStream, mimeType, cancellationToken)
					.ConfigureAwait(false);

				// set TCP and threadpool stats on the response here so that in the event the request fails after the point of
				// gathering stats, they are still exposed on the call details. Ideally these would be set inside ResponseBuilder.ToResponse,
				// but doing so would be a breaking change in 7.x
				response.ApiCall.TcpStats = tcpStats;
				response.ApiCall.ThreadPoolStats = threadPoolStats;
				return response;
			}
		}

		void IDisposable.Dispose() => DisposeManagedResources();

		private HttpClient GetClient(RequestData requestData) => HttpClientFactory.CreateClient(requestData);

		protected virtual HttpMessageHandler CreateHttpClientHandler(RequestData requestData)
		{
			var handler = new HttpClientHandler { AutomaticDecompression = requestData.HttpCompression ? GZip | Deflate : None, };

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
			else if (requestData.DisableAutomaticProxyDetection) handler.UseProxy = false;

			var callback = requestData.ConnectionSettings?.ServerCertificateValidationCallback;
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
			var request = CreateRequestMessage(requestData);
			SetAuthenticationIfNeeded(request, requestData);
			return request;
		}

		protected virtual void SetAuthenticationIfNeeded(HttpRequestMessage requestMessage, RequestData requestData)
		{
			//If user manually specifies an Authorization Header give it preference
			if (requestData.Headers.HasKeys() && requestData.Headers.AllKeys.Contains("Authorization"))
			{
				var header = AuthenticationHeaderValue.Parse(requestData.Headers["Authorization"]);
				requestMessage.Headers.Authorization = header;
				return;
			};

			// Api Key authentication takes precedence
			var apiKeySet = SetApiKeyAuthenticationIfNeeded(requestMessage, requestData);

			if (!apiKeySet)
				SetBasicAuthenticationIfNeeded(requestMessage, requestData);
		}

		// TODO - make private in 8.0 and only expose SetAuthenticationIfNeeded
		protected virtual bool SetApiKeyAuthenticationIfNeeded(HttpRequestMessage requestMessage, RequestData requestData)
		{
			// ApiKey auth credentials take the following precedence (highest -> lowest):
			// 1 - Specified on the request (highest precedence)
			// 2 - Specified at the global IConnectionSettings level


			string apiKey = null;
			if (requestData.ApiKeyAuthenticationCredentials != null)
				apiKey = requestData.ApiKeyAuthenticationCredentials.Base64EncodedApiKey.CreateString();

			if (string.IsNullOrWhiteSpace(apiKey))
				return false;

			requestMessage.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", apiKey);
			return true;

		}

		// TODO - make private in 8.0 and only expose SetAuthenticationIfNeeded
		protected virtual void SetBasicAuthenticationIfNeeded(HttpRequestMessage requestMessage, RequestData requestData)
		{
			// Basic auth credentials take the following precedence (highest -> lowest):
			// 1 - Specified on the request (highest precedence)
			// 2 - Specified at the global IConnectionSettings level
			// 3 - Specified with the URI (lowest precedence)

			string userInfo = null;
			if (!requestData.Uri.UserInfo.IsNullOrEmpty())
				userInfo = Uri.UnescapeDataString(requestData.Uri.UserInfo);
			else if (requestData.BasicAuthorizationCredentials != null)
				userInfo =
					$"{requestData.BasicAuthorizationCredentials.Username}:{requestData.BasicAuthorizationCredentials.Password.CreateString()}";
			if (!userInfo.IsNullOrEmpty())
			{
				var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
				requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
			}
		}

		protected virtual HttpRequestMessage CreateRequestMessage(RequestData requestData)
		{
			var method = ConvertHttpMethod(requestData.Method);
			var requestMessage = new HttpRequestMessage(method, requestData.Uri);

			if (requestData.Headers != null)
				foreach (string key in requestData.Headers)
					requestMessage.Headers.TryAddWithoutValidation(key, requestData.Headers.GetValues(key));

			requestMessage.Headers.Connection.Clear();
			requestMessage.Headers.ConnectionClose = false;
			requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(requestData.Accept));

			if (!string.IsNullOrWhiteSpace(requestData.UserAgent))
			{
				requestMessage.Headers.UserAgent.Clear();
				requestMessage.Headers.UserAgent.TryParseAdd(requestData.UserAgent);
			}

			if (!requestData.RunAs.IsNullOrEmpty())
				requestMessage.Headers.Add(RequestData.RunAsSecurityHeader, requestData.RunAs);

			return requestMessage;
		}

		private static void SetContent(HttpRequestMessage message, RequestData requestData)
		{
			if (requestData.TransferEncodingChunked)
			{
				message.Content = new RequestDataContent(requestData);
			}
			else
			{
				var stream = requestData.MemoryStreamFactory.Create();
				if (requestData.HttpCompression)
					using (var zipStream = new GZipStream(stream, CompressionMode.Compress, true))
						requestData.PostData.Write(zipStream, requestData.ConnectionSettings);
				else
					requestData.PostData.Write(stream, requestData.ConnectionSettings);

				// the written bytes are uncompressed, so can only be used when http compression isn't used
				if (requestData.PostData.DisableDirectStreaming.GetValueOrDefault(false) && !requestData.HttpCompression)
				{
					message.Content = new ByteArrayContent(requestData.PostData.WrittenBytes);
					stream.Dispose();
				}
				else
				{
					stream.Position = 0;
					message.Content = new StreamContent(stream);
				}

				if (requestData.HttpCompression)
					message.Content.Headers.ContentEncoding.Add("gzip");

				message.Content.Headers.ContentType = new MediaTypeHeaderValue(requestData.RequestMimeType);
			}
		}

		private static async Task SetContentAsync(HttpRequestMessage message, RequestData requestData, CancellationToken cancellationToken)
		{
			if (requestData.TransferEncodingChunked)
			{
				message.Content = new RequestDataContent(requestData, cancellationToken);
			}
			else
			{
				var stream = requestData.MemoryStreamFactory.Create();
				if (requestData.HttpCompression)
					using (var zipStream = new GZipStream(stream, CompressionMode.Compress, true))
						await requestData.PostData.WriteAsync(zipStream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);
				else
					await requestData.PostData.WriteAsync(stream, requestData.ConnectionSettings, cancellationToken).ConfigureAwait(false);

				// the written bytes are uncompressed, so can only be used when http compression isn't used
				if (requestData.PostData.DisableDirectStreaming.GetValueOrDefault(false) && !requestData.HttpCompression)
				{
					message.Content = new ByteArrayContent(requestData.PostData.WrittenBytes);
					stream.Dispose();
				}
				else
				{
					stream.Position = 0;
					message.Content = new StreamContent(stream);
				}

				if (requestData.HttpCompression)
					message.Content.Headers.ContentEncoding.Add("gzip");

				message.Content.Headers.ContentType = new MediaTypeHeaderValue(requestData.RequestMimeType);
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

		internal static int GetClientKey(RequestData requestData)
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

		protected virtual void DisposeManagedResources() => HttpClientFactory.Dispose();
	}
}
#endif
