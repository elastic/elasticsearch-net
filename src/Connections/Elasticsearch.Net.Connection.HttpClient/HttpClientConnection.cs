using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection
{
	/// <summary>
	/// IConnection implemented using <see cref="System.Net.Http.HttpClient"/>
	/// </summary>
	public class HttpClientConnection : IConnection, IDisposable
	{
		private readonly IConnectionConfigurationValues _settings;

		static HttpClientConnection()
		{
			// brought over from HttpClient
			ServicePointManager.UseNagleAlgorithm = false;
			ServicePointManager.Expect100Continue = false;

			// this should be set globally based on _settings.MaximumAsyncConnections
			ServicePointManager.DefaultConnectionLimit = 10000;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpClientConnection"/> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="handler">The handler.</param>
		public HttpClientConnection(IConnectionConfigurationValues settings, HttpClientHandler handler = null)
		{
			_settings = settings;
			DefaultContentType = "application/json";

			var innerHandler = handler ?? new WebRequestHandler();

			if (settings.EnableCompressedResponses)
				innerHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			if (innerHandler.SupportsProxy && !string.IsNullOrWhiteSpace(_settings.ProxyAddress))
			{
				innerHandler.Proxy = new WebProxy(_settings.ProxyAddress)
				{

					Credentials = new NetworkCredential(_settings.ProxyUsername, _settings.ProxyPassword),
				};

				innerHandler.UseProxy = true;
			}

			Client = new HttpClient(new InternalHttpMessageHandler(innerHandler), false)
			{
				Timeout = TimeSpan.FromMilliseconds(_settings.Timeout)
			};

		}

		/// <summary>
		/// Gets or sets the default type of the content.
		/// </summary>
		/// <value>The default type of the content.</value>
		public string DefaultContentType { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance is disposed.
		/// </summary>
		/// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value>
		public bool IsDisposed { get; private set; }

		/// <summary>
		/// Gets the client.
		/// </summary>
		/// <value>The client.</value>
		public HttpClient Client { get; private set; }

		/// <summary>
		/// Wraps the DoRequest to run synchronously
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="uri">The URI.</param>
		/// <param name="data">The data.</param>
		/// <param name="requestSpecificConfig">The request specific configuration.</param>
		/// <returns>ElasticsearchResponse&lt;Stream&gt;.</returns>
		public ElasticsearchResponse<Stream> DoRequestSync(HttpMethod method, Uri uri, byte[] data = null, IRequestConfiguration requestSpecificConfig = null)
		{
			ThrowIfDisposed();

			var requestTask = DoRequest(method, uri, data, requestSpecificConfig);

			try
			{
				requestTask.Wait();
				return requestTask.Result;
			}
			catch (AggregateException ex)
			{
				return ElasticsearchResponse<Stream>.CreateError(_settings, ex.Flatten(), method.ToString().ToLowerInvariant(), uri.ToString(), data);
			}
			catch (Exception ex)
			{
				return ElasticsearchResponse<Stream>.CreateError(_settings, ex, method.ToString().ToLowerInvariant(), uri.ToString(), data);
			}
		}

		/// <summary>
		/// Makes an async call to the specified url. Uses the timeout from the IRequestSpecifiConfig is supplied, or the global timeout from settings.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <param name="uri">The URI.</param>
		/// <param name="data">The data.</param>
		/// <param name="requestSpecificConfig">The request specific configuration.</param>
		/// <returns>Task&lt;ElasticsearchResponse&lt;Stream&gt;&gt;.</returns>
		public async Task<ElasticsearchResponse<Stream>> DoRequest(HttpMethod method, Uri uri, byte[] data = null, IRequestConfiguration requestSpecificConfig = null)
		{
			ThrowIfDisposed();

			try
			{
				var request = new HttpRequestMessage(method, uri);
				if (requestSpecificConfig != null && !string.IsNullOrWhiteSpace(requestSpecificConfig.ContentType))
					request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(requestSpecificConfig.ContentType));
				else if (!string.IsNullOrWhiteSpace(DefaultContentType))
					request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultContentType));

				if (!string.IsNullOrWhiteSpace(DefaultContentType))
					request.Content.Headers.ContentType = new MediaTypeHeaderValue(DefaultContentType);

				if (!string.IsNullOrEmpty(uri.UserInfo))
				{
					request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(uri.UserInfo)));
				}

				if (method != HttpMethod.Get && method != HttpMethod.Head && data != null && data.Length > 0)
				{
					request.Content = new ByteArrayContent(data);
				}

				var response = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

				if (method == HttpMethod.Head || response.Content == null || !response.Content.Headers.ContentLength.HasValue || response.Content.Headers.ContentLength.Value <= 0)
				{
					return ElasticsearchResponse<Stream>.Create(_settings, (int)response.StatusCode, method.ToString().ToLowerInvariant(), uri.ToString(), data);
				}

				var responseStream = await response.Content.ReadAsStreamAsync();
				return ElasticsearchResponse<Stream>.Create(_settings, (int)response.StatusCode, method.ToString().ToLowerInvariant(), uri.ToString(), data, responseStream);

			}
			catch (Exception ex)
			{
				return ElasticsearchResponse<Stream>.CreateError(_settings, ex, method.ToString().ToLowerInvariant(), uri.ToString(), data);
			}
		}

		Task<ElasticsearchResponse<Stream>> IConnection.Get(Uri uri, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequest(HttpMethod.Get, uri, null, requestSpecificConfig);
		}

		ElasticsearchResponse<Stream> IConnection.GetSync(Uri uri, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequestSync(HttpMethod.Get, uri, null, requestSpecificConfig);
		}

		Task<ElasticsearchResponse<Stream>> IConnection.Head(Uri uri, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequest(HttpMethod.Head, uri, null, requestSpecificConfig);
		}

		ElasticsearchResponse<Stream> IConnection.HeadSync(Uri uri, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequestSync(HttpMethod.Head, uri, null, requestSpecificConfig);
		}

		Task<ElasticsearchResponse<Stream>> IConnection.Post(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequest(HttpMethod.Post, uri, data, requestSpecificConfig);
		}

		ElasticsearchResponse<Stream> IConnection.PostSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequestSync(HttpMethod.Post, uri, data, requestSpecificConfig);
		}

		Task<ElasticsearchResponse<Stream>> IConnection.Put(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequest(HttpMethod.Put, uri, data, requestSpecificConfig);
		}

		ElasticsearchResponse<Stream> IConnection.PutSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequestSync(HttpMethod.Put, uri, data, requestSpecificConfig);
		}

		Task<ElasticsearchResponse<Stream>> IConnection.Delete(Uri uri, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequest(HttpMethod.Delete, uri, null, requestSpecificConfig);
		}

		ElasticsearchResponse<Stream> IConnection.DeleteSync(Uri uri, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequestSync(HttpMethod.Delete, uri, null, requestSpecificConfig);
		}

		Task<ElasticsearchResponse<Stream>> IConnection.Delete(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequest(HttpMethod.Delete, uri, data, requestSpecificConfig);
		}

		ElasticsearchResponse<Stream> IConnection.DeleteSync(Uri uri, byte[] data, IRequestConfiguration requestSpecificConfig)
		{
			return DoRequestSync(HttpMethod.Delete, uri, data, requestSpecificConfig);
		}

		private void ThrowIfDisposed()
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~HttpClientConnection()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed)
				return;

			if (disposing)
			{
				if (Client != null)
				{
					Client.Dispose();
					Client = null;
				}
			}

			IsDisposed = true;
		}
	}
}
