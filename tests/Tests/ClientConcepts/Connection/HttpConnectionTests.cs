/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

#if DOTNETCORE
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch;
using Tests.Core.ManagedElasticsearch.Clusters;
using HttpMethod = Elasticsearch.Net.HttpMethod;
using FluentAssertions;
using System.Text.RegularExpressions;
using Environment = System.Environment;

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
			connection.InUseHandlers.Should().Be(1);
			connection.RemovedHandlers.Should().Be(0);

			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);

			connection.CallCount.Should().Be(2);
			connection.InUseHandlers.Should().Be(1);
		}

		[I] public async Task RespectsDnsRefreshTimeout()
		{
			var connection = new TestableHttpConnection();
			connection.RemovedHandlers.Should().Be(0);
			var requestData = CreateRequestData(dnsRefreshTimeout: TimeSpan.FromSeconds(1));
			connection.Request<StringResponse>(requestData);
			await Task.Delay(TimeSpan.FromSeconds(2));
			connection.Request<StringResponse>(requestData);

			connection.CallCount.Should().Be(2);
			connection.InUseHandlers.Should().Be(1);
			connection.RemovedHandlers.Should().Be(1);

			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);

			connection.CallCount.Should().Be(3);
			connection.InUseHandlers.Should().Be(1);
			connection.RemovedHandlers.Should().Be(1);
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
			connection.InUseHandlers.Should().Be(1);
			connection.RemovedHandlers.Should().Be(0);

			requestData = differentRequestData();
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);

			connection.CallCount.Should().Be(2);
			connection.InUseHandlers.Should().Be(2);
			connection.RemovedHandlers.Should().Be(0);
		}

		private RequestData CreateRequestData(
			TimeSpan requestTimeout = default,
			TimeSpan? dnsRefreshTimeout = default,
			Uri proxyAddress = null,
			bool disableAutomaticProxyDetection = false,
			bool httpCompression = false,
			bool transferEncodingChunked = false,
			bool disableMetaHeader = false,
			bool enableApiVersioning = false,
			Action<RequestMetaData> requestMetaData = null
		)
		{
			if (requestTimeout == default) requestTimeout = TimeSpan.FromSeconds(10);

			var node = Client.ConnectionSettings.ConnectionPool.Nodes.First();
			var connectionSettings = new ConnectionSettings(node.Uri)
				.RequestTimeout(requestTimeout)
				.DnsRefreshTimeout(dnsRefreshTimeout ?? ConnectionConfiguration.DefaultDnsRefreshTimeout)
				.DisableAutomaticProxyDetection(disableAutomaticProxyDetection)
				.TransferEncodingChunked(transferEncodingChunked)
				.EnableHttpCompression(httpCompression)
				.EnableApiVersioningHeader(enableApiVersioning)
				.DisableMetaHeader(disableMetaHeader);

			if (proxyAddress != null)
				connectionSettings.Proxy(proxyAddress, null, (string)null);

			var requestParameters = new SearchRequestParameters();

			if (requestMetaData is object)
			{
				requestParameters.RequestConfiguration ??= new RequestConfiguration();
				requestParameters.RequestConfiguration.RequestMetaData ??= new RequestMetaData();
				requestMetaData(requestParameters.RequestConfiguration.RequestMetaData);
			}

			var requestData = new RequestData(HttpMethod.POST, "/_search", "{ \"query\": { \"match_all\" : { } } }", connectionSettings,
				requestParameters,
				new RecyclableMemoryStreamFactory()) { Node = node };

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

		[I] public async Task HttpClientSetsMetaHeaderWhenNotDisabled()
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

		[I] public async Task HttpClientSetsMetaHeaderWithHelperWhenNotDisabled()
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

		[I] public async Task HttpClientShouldNotSetMetaHeaderWhenDisabled()
		{
			var requestData = CreateRequestData(disableMetaHeader: true);
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Headers.TryGetValues("x-elastic-client-meta", out var headerValue).Should().BeFalse();
			});

			connection.Request<StringResponse>(requestData);
			await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
		}

		[I] public async Task HttpClientSetsApiVersioningHeaderWhenEnabled()
		{
			var requestData = CreateRequestData(enableApiVersioning: true);
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentType.Should().NotBeNull();
				var contentType = responseMessage.RequestMessage.Content.Headers.ContentType.ToString();
				contentType.Should().Contain("compatible-with");

			});

			var r = connection.Request<StringResponse>(requestData);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
			r = await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
		}

		[I] public async Task HttpClientSetsApiVersioningHeaderWhenDisabled()
		{
			var requestData = CreateRequestData(enableApiVersioning: false);
			var connection = new TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentType.Should().NotBeNull();
				var contentType = responseMessage.RequestMessage.Content.Headers.ContentType.ToString();
				//application/json in v7 otherwise vendored by default
				contentType.Should().StartWith(RequestData.DefaultJsonMimeType);

			});

			var r = connection.Request<StringResponse>(requestData);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
			r = await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
		}

		public class TestableHttpConnection : HttpConnection
		{
			private readonly Action<HttpResponseMessage> _response;
			private TestableClientHandler _handler;
			public int CallCount { get; private set; }
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

	public class HttpConnectionEnvironmentalTests : ClusterTestClassBase<ReadOnlyCluster>, IDisposable
	{
		private readonly string _previousEnvironmentVariable;

		public HttpConnectionEnvironmentalTests(ReadOnlyCluster cluster) : base(cluster) =>
			_previousEnvironmentVariable = Environment.GetEnvironmentVariable(ConnectionConfiguration.ApiVersioningEnvironmentVariableName);

		[I]
		public async Task HttpClientSetsApiVersioningHeaderWhenEnabledAsTrue()
		{
			Environment.SetEnvironmentVariable(ConnectionConfiguration.ApiVersioningEnvironmentVariableName, "true");

			var requestData = CreateRequestData();
			var connection = new HttpConnectionTests.TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentType.Should().NotBeNull();
				var contentType = responseMessage.RequestMessage.Content.Headers.ContentType.ToString();
				contentType.Should().Contain("compatible-with");

			});

			var r = connection.Request<StringResponse>(requestData);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
			r = await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
		}

		[I]
		public async Task HttpClientSetsApiVersioningHeaderWhenEnabledAsOne()
		{
			Environment.SetEnvironmentVariable(ConnectionConfiguration.ApiVersioningEnvironmentVariableName, "1");

			var requestData = CreateRequestData();
			var connection = new HttpConnectionTests.TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentType.Should().NotBeNull();
				var contentType = responseMessage.RequestMessage.Content.Headers.ContentType.ToString();
				contentType.Should().Contain("compatible-with");

			});

			var r = connection.Request<StringResponse>(requestData);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
			r = await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
		}

		[I]
		public async Task HttpClientSetsApiVersioningHeaderWhenDisabled()
		{
			Environment.SetEnvironmentVariable(ConnectionConfiguration.ApiVersioningEnvironmentVariableName, null);

			var requestData = CreateRequestData();
			var connection = new HttpConnectionTests.TestableHttpConnection(responseMessage =>
			{
				responseMessage.RequestMessage.Content.Headers.ContentType.Should().NotBeNull();
				var contentType = responseMessage.RequestMessage.Content.Headers.ContentType.ToString();
				//application/json in v7 otherwise vendored by default
				contentType.Should().StartWith(RequestData.DefaultJsonMimeType);

			});

			var r = connection.Request<StringResponse>(requestData);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
			r = await connection.RequestAsync<StringResponse>(requestData, CancellationToken.None).ConfigureAwait(false);
			r.ApiCall.ResponseMimeType.Should().StartWith(RequestData.DefaultJsonMimeType);
		}

		public void Dispose() => Environment.SetEnvironmentVariable(ConnectionConfiguration.ApiVersioningEnvironmentVariableName, _previousEnvironmentVariable);

		private RequestData CreateRequestData()
		{
			var node = Client.ConnectionSettings.ConnectionPool.Nodes.First();
			var connectionSettings = new ConnectionSettings(node.Uri);

			var requestParameters = new SearchRequestParameters();

			var requestData = new RequestData(HttpMethod.POST, "/_search", "{ \"query\": { \"match_all\" : { } } }", connectionSettings,
					requestParameters,
					new RecyclableMemoryStreamFactory())
			{ Node = node };

			return requestData;
		}
	}
}
#endif
