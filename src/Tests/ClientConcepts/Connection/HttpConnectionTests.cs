#if DOTNETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net_5_2_0;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;

namespace Tests.ClientConcepts.Connection
{
    public class HttpConnectionTests
    {
	    public class TestableHttpConnection : HttpConnection
	    {
		    public int ClientCount => this.Clients.Count;

		    public int CallCount { get; private set; }

		    public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData)
		    {
			    CallCount++;
			    return base.Request<TReturn>(requestData);
		    }

		    public override Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData, CancellationToken cancellationToken)
		    {
			    CallCount++;
			    return base.RequestAsync<TReturn>(requestData, cancellationToken);
		    }
	    }

	    [U]
	    public async Task SingleInstanceOfHttpClient()
	    {
			var connection = new TestableHttpConnection();
		    var requestData = CreateRequestData(TimeSpan.FromMinutes(1));
			connection.Request<string>(requestData);

			connection.CallCount.Should().Be(1);
		    connection.ClientCount.Should().Be(1);

			await connection.RequestAsync<string>(requestData, CancellationToken.None).ConfigureAwait(false);

			connection.CallCount.Should().Be(2);
			connection.ClientCount.Should().Be(1);
		}

	    [U]
		public async Task MultipleInstancesOfHttpClientWhenRequestTimeoutChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(TimeSpan.FromSeconds(30)));
		}

		[U]
		public async Task MultipleInstancesOfHttpClientWhenProxyChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(proxyAddress: new Uri("http://localhost:9400")));
		}

		[U]
		public async Task MultipleInstancesOfHttpClientWhenAutomaticProxyDetectionChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(disableAutomaticProxyDetection: true));
		}

		[U]
		public async Task MultipleInstancesOfHttpClientWhenHttpCompressionChanges()
		{
			await MultipleInstancesOfHttpClientWhen(() => CreateRequestData(httpCompression: true));
		}

	    private static async Task MultipleInstancesOfHttpClientWhen(Func<RequestData> differentRequestData)
	    {
		    var connection = new TestableHttpConnection();
		    var requestData = CreateRequestData();
		    connection.Request<string>(requestData);

		    connection.CallCount.Should().Be(1);
		    connection.ClientCount.Should().Be(1);

		    requestData = differentRequestData();
		    await connection.RequestAsync<string>(requestData, CancellationToken.None).ConfigureAwait(false);

		    connection.CallCount.Should().Be(2);
		    connection.ClientCount.Should().Be(2);
	    }

	    private static RequestData CreateRequestData(
			TimeSpan requestTimeout = default(TimeSpan),
			Uri proxyAddress = null,
			bool disableAutomaticProxyDetection = false,
			bool httpCompression = false)
		{
			if (requestTimeout == default(TimeSpan))
				requestTimeout = TimeSpan.FromMinutes(1);

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
    }
}
#endif
