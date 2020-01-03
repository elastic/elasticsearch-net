#if DOTNETCORE
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch;
using Tests.Core.ManagedElasticsearch.Clusters;
using HttpMethod = Elasticsearch.Net.HttpMethod;
using FluentAssertions;

namespace Tests.ClientConcepts.Connection
{
	public class HttpConnectionTests : ClusterTestClassBase<ReadOnlyCluster>
	{
		public HttpConnectionTests(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public async Task SingleInstanceOfHttpClient()
		{
			var connection = new TestableHttpConnection();
			var requestData = CreateRequestData();
			connection.Request<StringResponse>(requestData);

			connection.CallCount.Should().Be(1);
			connection.ClientCount.Should().Be(1);

			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);

			connection.CallCount.Should().Be(2);
			connection.ClientCount.Should().Be(1);
		}

		[I] public async Task MultipleInstancesOfHttpClientWhenRequestTimeoutChanges() =>
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(TimeSpan.FromSeconds(30)));

		[I] public async Task MultipleInstancesOfHttpClientWhenProxyChanges() =>
			await MultipleInstancesOfHttpClientWhen(() =>
				CreateRequestData(proxyAddress: Client.ConnectionSettings.ConnectionPool.Nodes.First().Uri));

		[I] public async Task MultipleInstancesOfHttpClientWhenAutomaticProxyDetectionChanges() =>
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(disableAutomaticProxyDetection: true));

		[I] public async Task MultipleInstancesOfHttpClientWhenHttpCompressionChanges() =>
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(httpCompression: true));

		private async Task MultipleInstancesOfHttpClientWhen(Func<RequestData> differentRequestData)
		{
			var connection = new TestableHttpConnection();
			var requestData = CreateRequestData();
			connection.Request<StringResponse>(requestData);

			connection.CallCount.Should().Be(1);
			connection.ClientCount.Should().Be(1);

			requestData = differentRequestData();
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);

			connection.CallCount.Should().Be(2);
			connection.ClientCount.Should().Be(2);
		}

		private RequestData CreateRequestData(
			TimeSpan requestTimeout = default,
			Uri proxyAddress = null,
			bool disableAutomaticProxyDetection = false,
			bool httpCompression = false,
			bool transferEncodingChunked = false
		)
		{
			if (requestTimeout == default) requestTimeout = TimeSpan.FromSeconds(10);

			var node = Client.ConnectionSettings.ConnectionPool.Nodes.First();
			var connectionSettings = new ConnectionSettings(node.Uri)
				.RequestTimeout(requestTimeout)
				.DisableAutomaticProxyDetection(disableAutomaticProxyDetection)
				.TransferEncodingChunked(transferEncodingChunked)
				.EnableHttpCompression(httpCompression);

			if (proxyAddress != null)
				connectionSettings.Proxy(proxyAddress, null, (string)null);

			var requestData = new RequestData(HttpMethod.POST, "/_search", "{ \"query\": { \"match_all\" : { } } }", connectionSettings,
				new SearchRequestParameters(),
				new MemoryStreamFactory()) { Node = node };

			return requestData;
		}

		/// <summary>
		/// Setting HttpClientHandler.Proxy = null don't disable HttpClient automatic proxy detection.
		/// It is disabled by setting Proxy to non-null value or by setting UseProxy = false.
		/// </summary>
		[I] public async Task HttpClientUseProxyShouldBeFalseWhenDisabledAutoProxyDetection()
		{
			var connection = new TestableHttpConnection();
			var requestData = CreateRequestData(disableAutomaticProxyDetection: true);

			connection.Request<StringResponse>(requestData);
			connection.LastHttpClientHandler.UseProxy.Should().BeFalse();

			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			connection.LastHttpClientHandler.UseProxy.Should().BeFalse();
		}

		[I] public async Task HttpClientUseProxyShouldBeTrueWhenEnabledAutoProxyDetection()
		{
			var connection = new TestableHttpConnection();
			var requestData = CreateRequestData();

			connection.Request<StringResponse>(requestData);
			connection.LastHttpClientHandler.UseProxy.Should().BeTrue();
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			connection.LastHttpClientHandler.UseProxy.Should().BeTrue();
		}

		[I] public async Task HttpClientUseTransferEncodingChunkedWhenTransferEncodingChunkedTrue()
		{
			var requestData = CreateRequestData(transferEncodingChunked: true);
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentLength.Should().BeNull();
			});

			connection.Request<StringResponse>(requestData);
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
		}

		[I] public async Task HttpClientSetsContentLengthWhenTransferEncodingChunkedFalse()
		{
			var requestData = CreateRequestData(transferEncodingChunked: false);
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentLength.Should().HaveValue();
			});

			connection.Request<StringResponse>(requestData);
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
		}

		[I] public async Task HttpClientSetsContentLengthWhenTransferEncodingChunkedHttpCompression()
		{
			var requestData = CreateRequestData(transferEncodingChunked: false, httpCompression: true);
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentLength.Should().HaveValue();
			});

			connection.Request<StringResponse>(requestData);
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
		}

		public class TestableHttpConnection : HttpConnection
		{
			private readonly Action<HttpResponseMessage> _response;
			private TestableClientHandler _handler;
			public int CallCount { get; private set; }
			public int ClientCount => Clients.Count;
			public HttpClientHandler LastHttpClientHandler => (HttpClientHandler)_handler.InnerHandler;

			public TestableHttpConnection(Action<HttpResponseMessage> response) => _response = response;

			public TestableHttpConnection()
			{
			}

			public override TResponse Request<TResponse>(RequestData requestData)
			{
				CallCount++;
				return base.Request<TResponse>(requestData);
			}

			public override Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			{
				CallCount++;
				return base.RequestAsync<TResponse>(requestData, cancellationToken);
			}

			protected override HttpMessageHandler CreateHttpClientHandler(RequestData requestData)
			{
				_handler = new TestableClientHandler(base.CreateHttpClientHandler(requestData), _response);
				return _handler;
			}

			protected override void DisposeManagedResources()
			{
				_handler?.Dispose();
				base.DisposeManagedResources();
			}
		}

		public class TestableClientHandler : DelegatingHandler
		{
			private readonly Action<HttpResponseMessage> _responseAction;

			public TestableClientHandler(HttpMessageHandler handler, Action<HttpResponseMessage> responseAction) : base(handler) =>
				_responseAction = responseAction;

			protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
			{
				var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
				_responseAction?.Invoke(response);
				return response;
			}
		}
	}
}
#endif
