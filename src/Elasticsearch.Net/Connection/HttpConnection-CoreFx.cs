using System;
using System.IO.Compression;
#if DOTNETCORE
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
			throw new NotImplementedException();
		}

		public virtual Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData) where TReturn : class
		{
			throw new NotImplementedException();
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
