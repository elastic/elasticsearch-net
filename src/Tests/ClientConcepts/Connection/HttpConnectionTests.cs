using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using HttpMethod = Elasticsearch.Net.HttpMethod;

namespace Tests.ClientConcepts.Connection
{
    public class HttpConnectionTests
    {
	    public class TestableHttpConnection : HttpConnection
	    {
		    public int ClientCount => this.Clients.Count;

		    public int CallCount { get; private set; }

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
			    LastUsedHttpClientHandler = base.CreateHttpClientHandler(requestData);
			    return LastUsedHttpClientHandler;
		    }
	    }

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

	    [I] public async Task MultipleInstancesOfHttpClientWhenRequestTimeoutChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(TimeSpan.FromSeconds(30)));
		}

		[I] public async Task MultipleInstancesOfHttpClientWhenProxyChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(proxyAddress: new Uri("http://localhost:9400")));
		}

		[I] public async Task MultipleInstancesOfHttpClientWhenAutomaticProxyDetectionChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(disableAutomaticProxyDetection: true));
		}

		[I] public async Task MultipleInstancesOfHttpClientWhenHttpCompressionChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(httpCompression: true));
		}

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
			bool httpCompression = false)
		{
			if (requestTimeout == default(TimeSpan)) requestTimeout = TimeSpan.FromSeconds(1);

			var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"))
				.RequestTimeout(requestTimeout)
				.DisableAutomaticProxyDetection(disableAutomaticProxyDetection)
				.EnableHttpCompression(httpCompression);

			if (proxyAddress != null)
				connectionSettings.Proxy(proxyAddress, null, null);

			var requestData = new RequestData(HttpMethod.GET, "/", null, connectionSettings, new PingRequestParameters(),
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
    }
}
