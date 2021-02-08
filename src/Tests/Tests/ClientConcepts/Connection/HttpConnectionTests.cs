#if DOTNETCORE
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using HttpMethod = Elasticsearch.Net.HttpMethod;

namespace Tests.ClientConcepts.Connection
{
	public class HttpConnectionTests
	{
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
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(proxyAddress: new Uri("http://localhost:9400")));

		[I] public async Task MultipleInstancesOfHttpClientWhenAutomaticProxyDetectionChanges() =>
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(disableAutomaticProxyDetection: true));

		[I] public async Task MultipleInstancesOfHttpClientWhenHttpCompressionChanges() =>
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(httpCompression: true));

		private static async Task MultipleInstancesOfHttpClientWhen(Func<RequestData> differentRequestData)
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

		private static RequestData CreateRequestData(
			TimeSpan requestTimeout = default(TimeSpan),
			Uri proxyAddress = null,
			bool disableAutomaticProxyDetection = false,
			bool httpCompression = false,
			bool disableMetaHeader = false,
			Action<RequestMetaData> requestMetaData = null
		)
		{
			if (requestTimeout == default(TimeSpan)) requestTimeout = TimeSpan.FromSeconds(10);

			var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"))
				.RequestTimeout(requestTimeout)
				.DisableAutomaticProxyDetection(disableAutomaticProxyDetection)
				.EnableHttpCompression(httpCompression)
				.DisableMetaHeader(disableMetaHeader);

			if (proxyAddress != null)
				connectionSettings.Proxy(proxyAddress, null, null);

			var requestParameters = new SearchRequestParameters();

			if (requestMetaData is object)
			{
				requestParameters.RequestConfiguration ??= new RequestConfiguration();
				requestParameters.RequestConfiguration.RequestMetaData ??= new RequestMetaData();
				requestMetaData(requestParameters.RequestConfiguration.RequestMetaData);
			}

			var requestData = new RequestData(HttpMethod.GET, "/", null, connectionSettings, requestParameters, 
				new MemoryStreamFactory())
			{
				Node = new Node(new Uri("http://localhost:9200"))
			};
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
			connection.LastUsedHttpClientHandler.UseProxy.Should().BeFalse();

			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			connection.LastUsedHttpClientHandler.UseProxy.Should().BeFalse();
		}

		[I] public async Task HttpClientUseProxyShouldBeTrueWhenEnabledAutoProxyDetection()
		{
			var connection = new TestableHttpConnection();
			var requestData = CreateRequestData();

			connection.Request<StringResponse>(requestData);
			connection.LastUsedHttpClientHandler.UseProxy.Should().BeTrue();

			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			connection.LastUsedHttpClientHandler.UseProxy.Should().BeTrue();
		}

		[U] public async Task HttpClientSetsMetaHeaderWhenNotDisabled()
		{
			var regex = new Regex(@"^[a-z]{1,}=[a-z0-9\.\-]{1,}(?:,[a-z]{1,}=[a-z0-9\.\-]+)*$");

			var requestData = CreateRequestData();
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Headers.TryGetValues("x-elastic-client-meta", out var headerValue).Should().BeTrue();
				headerValue.Should().HaveCount(1);
				headerValue.Single().Should().NotBeNullOrEmpty();
				regex.Match(headerValue.Single()).Success.Should().BeTrue();
			});

			connection.Request<StringResponse>(requestData);
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
		}

		[U] public async Task HttpClientSetsMetaHeaderWithHelperWhenNotDisabled()
		{
			var regex = new Regex(@"^[a-z]{1,}=[a-z0-9\.\-]{1,}(?:,[a-z]{1,}=[a-z0-9\.\-]+)*$");

			var requestData = CreateRequestData(requestMetaData: m => m.TryAddMetaData("helper", "r"));
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Headers.TryGetValues("x-elastic-client-meta", out var headerValue).Should().BeTrue();
				headerValue.Should().HaveCount(1);
				headerValue.Single().Should().NotBeNullOrEmpty();
				headerValue.Single().Should().EndWith(",h=r");
				regex.Match(headerValue.Single()).Success.Should().BeTrue();
			});

			connection.Request<StringResponse>(requestData);
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
		}

		[U] public async Task HttpClientShouldNotSetMetaHeaderWhenDisabled()
		{
			var requestData = CreateRequestData(disableMetaHeader: true);
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Headers.TryGetValues("x-elastic-client-meta", out var headerValue).Should().BeFalse();
			});

			connection.Request<StringResponse>(requestData);
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
		}

		public class TestableHttpConnection : HttpConnection
		{
			private readonly Action<HttpResponseMessage> _response;
			
			private TestableClientHandler _handler;
			
			public TestableHttpConnection(Action<HttpResponseMessage> response) => _response = response;

			public TestableHttpConnection() {
			}

			public int CallCount { get; private set; }
			public int ClientCount => Clients.Count;

			public HttpClientHandler LastUsedHttpClientHandler { get; private set; }

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

			protected override HttpClientHandler CreateHttpClientHandler(RequestData requestData)
			{
				_handler = new TestableClientHandler(base.CreateHttpClientHandler(requestData), _response);
				return _handler;
			}
		}

		public class TestableClientHandler : HttpClientHandler {
			private readonly Action<HttpResponseMessage> _responseAction;

			public TestableClientHandler(HttpMessageHandler handler, Action<HttpResponseMessage> responseAction) =>
				_responseAction = responseAction;

			protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
				var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
				_responseAction?.Invoke(response);
				return response;
			}
		}
	}
}
#endif
