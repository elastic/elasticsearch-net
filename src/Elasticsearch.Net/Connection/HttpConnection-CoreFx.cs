#if DOTNETCORE
using System;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class HttpConnection : IConnection
	{
		private class WebProxy : IWebProxy
		{
			private readonly Uri _uri;

			public WebProxy(Uri uri) { _uri = uri; }

			public ICredentials Credentials { get; set; }

			public Uri GetProxy(Uri destination) => _uri;

			public bool IsBypassed(Uri host) => host.IsLoopback;
		}

		private readonly IConnectionConfigurationValues _settings;
		private readonly HttpClient _client;

		private string DefaultContentType => "application/json";

		public HttpConnection() { }

		public HttpConnection(IConnectionConfigurationValues settings, HttpClientHandler handler = null)
		{
			_settings = settings;

			var innerHandler = handler ?? new HttpClientHandler();

			if (innerHandler.SupportsProxy && !string.IsNullOrWhiteSpace(_settings.ProxyAddress))
			{
				var proxy = new Uri(_settings.ProxyAddress);
				innerHandler.Proxy = new WebProxy(proxy)
				{
					Credentials = new NetworkCredential(_settings.ProxyUsername, _settings.ProxyPassword)
				};
				innerHandler.UseProxy = true;
			}

			this._client = new HttpClient(new InternalHttpMessageHandler(innerHandler), false)
			{
				Timeout = _settings.RequestTimeout
			};
			if (settings.EnableHttpCompression && innerHandler.SupportsAutomaticDecompression)
			{
				innerHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				this._client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
				this._client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
			}

		}
		public virtual ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData) where TReturn : class
		{
			var builder = new ResponseBuilder<TReturn>(requestData);
			try
			{
				var requestMessage = CreateHttpRequestMessage(requestData);
				var data = requestData.PostData;

				if (data != null)
				{
					using (var stream = requestData.MemoryStreamFactory.Create())
					{
						if (requestData.HttpCompression)
							using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
								data.Write(zipStream, requestData.ConnectionSettings);
						else
							data.Write(stream, requestData.ConnectionSettings);

						requestMessage.Content = new StreamContent(stream);
					}
				}

				var response = this._client.SendAsync(requestMessage).Result;
				builder.StatusCode = (int)response.StatusCode;
				builder.Stream = response.Content.ReadAsStreamAsync().Result;
			}
			catch (HttpRequestException e)
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
				var requestMessage = CreateHttpRequestMessage(requestData);
				var data = requestData.PostData;

				if (data != null)
				{
					using (var stream = requestData.MemoryStreamFactory.Create())
					{
						if (requestData.HttpCompression)
							using (var zipStream = new GZipStream(stream, CompressionMode.Compress))
								data.Write(zipStream, requestData.ConnectionSettings);
						else
							data.Write(stream, requestData.ConnectionSettings);

						requestMessage.Content = new StreamContent(stream);
					}
				}

				var response = await this._client.SendAsync(requestMessage);
				builder.StatusCode = (int)response.StatusCode;
				builder.Stream = await response.Content.ReadAsStreamAsync();
			}
			catch (HttpRequestException e)
			{
				HandleException(builder, e);
			}

			return builder.ToResponse();
		}

		private void HandleException<TReturn>(ResponseBuilder<TReturn> builder, HttpRequestException exception)
			where TReturn : class
		{
			builder.Exception = exception;

			// TODO: Figure out what to do here
			//var response = exception. as HttpWebResponse;
			//if (response != null)
			//{
			//	builder.StatusCode = (int)response.StatusCode;
			//	builder.Stream = response.GetResponseStream();
			//}
		}

		private static HttpRequestMessage CreateHttpRequestMessage(RequestData requestData)
		{
			var method = ConvertHttpMethod(requestData.Method);
			return new HttpRequestMessage(method, requestData.Uri);
		}

		private static System.Net.Http.HttpMethod ConvertHttpMethod(HttpMethod httpMethod)
		{
			switch (httpMethod)
			{
				case HttpMethod.GET:
					return System.Net.Http.HttpMethod.Get;
				case HttpMethod.POST:
					return System.Net.Http.HttpMethod.Post;
				case HttpMethod.PUT:
					return System.Net.Http.HttpMethod.Put;
				case HttpMethod.DELETE:
					return System.Net.Http.HttpMethod.Delete;
				case HttpMethod.HEAD:
					return System.Net.Http.HttpMethod.Head;
				default:
					throw new ArgumentException("Invalid value for HttpMethod", nameof(httpMethod));
			}
		}

		/// <summary>
		/// Internal Message Handler that is used by the HttpClientConnection. This class cannot be inherited.
		/// </summary>
		internal sealed class InternalHttpMessageHandler : DelegatingHandler
		{
			/// <summary>
			/// Creates a new instance of the <see cref="T:System.Net.Http.DelegatingHandler" /> class with a specific inner handler.
			/// </summary>
			/// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
			public InternalHttpMessageHandler(HttpClientHandler innerHandler)
				: base(innerHandler ?? new HttpClientHandler())
			{
			}

			/// <summary>
			/// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
			/// </summary>
			/// <param name="request">The HTTP request message to send to the server.</param>
			/// <param name="cancellationToken">A cancellation token to cancel operation.</param>
			/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.</returns>
			protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
			{
				if (request != null && request.RequestUri != null && !string.IsNullOrWhiteSpace(request.RequestUri.UserInfo))
				{
					request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(request.RequestUri.UserInfo)));
				}

				return base.SendAsync(request, cancellationToken);
			}
		}

	}
}
#endif
