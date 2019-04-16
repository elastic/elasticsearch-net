using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client.Settings;

namespace Tests.ClientConcepts.ConnectionPooling.Dispose
{
	public class ResponseBuilderDisposeTests
	{
		private readonly IConnectionSettingsValues _settings = new AlwaysInMemoryConnectionSettings().DisableDirectStreaming(false);
		private readonly IConnectionSettingsValues _settingsDisableDirectStream = new AlwaysInMemoryConnectionSettings().DisableDirectStreaming(true);

		[U] public async Task ResponseWithHttpStatusCode() => await AssertRegularResponse(false, 1);

		[U] public async Task ResponseBuilderWithNoHttpStatusCode() => await AssertRegularResponse(false);

		[U] public async Task ResponseWithHttpStatusCodeDisableDirectStreaming() =>
			await AssertRegularResponse(true, 1);

		[U] public async Task ResponseBuilderWithNoHttpStatusCodeDisableDirectStreaming() =>
			await AssertRegularResponse(true);

		private async Task AssertRegularResponse(bool disableDirectStreaming, int? statusCode = null)
		{
			var settings = disableDirectStreaming ? _settingsDisableDirectStream : _settings;
			var memoryStreamFactory = new TrackMemoryStreamFactory();
			var requestData = new RequestData(HttpMethod.GET, "/", null, settings, null, memoryStreamFactory)
			{
				Node = new Node(new Uri("http://localhost:9200"))
			};

			var stream = new TrackDisposeStream();
			var response = ResponseBuilder.ToResponse<RootNodeInfoResponse>(requestData, null, statusCode, null, stream);

			memoryStreamFactory.Created.Count().Should().Be(disableDirectStreaming ? 1 : 0);
			if (disableDirectStreaming)
			{
				var memoryStream = memoryStreamFactory.Created[0];
				memoryStream.IsDisposed.Should().BeTrue();
			}
			stream.IsDisposed.Should().BeTrue();


			stream = new TrackDisposeStream();
			var ct = new CancellationToken();
			response = await ResponseBuilder.ToResponseAsync<RootNodeInfoResponse>(requestData, null, statusCode, null, stream,
				cancellationToken: ct);
			memoryStreamFactory.Created.Count().Should().Be(disableDirectStreaming ? 2 : 0);
			if (disableDirectStreaming)
			{
				var memoryStream = memoryStreamFactory.Created[1];
				memoryStream.IsDisposed.Should().BeTrue();
			}
			stream.IsDisposed.Should().BeTrue();
		}

		[U] public async Task StreamResponseWithHttpStatusCode() => await AssertStreamResponse(false, 200);

		[U] public async Task StreamResponseBuilderWithNoHttpStatusCode() => await AssertStreamResponse(false);

		[U] public async Task StreamResponseWithHttpStatusCodeDisableDirectStreaming() =>
			await AssertStreamResponse(true, 1);

		[U] public async Task StreamResponseBuilderWithNoHttpStatusCodeDisableDirectStreaming() =>
			await AssertStreamResponse(true);

		private async Task AssertStreamResponse(bool disableDirectStreaming, int? statusCode = null)
		{
			var settings = disableDirectStreaming ? _settingsDisableDirectStream : _settings;
			var memoryStreamFactory = new TrackMemoryStreamFactory();

			var requestData = new RequestData(HttpMethod.GET, "/", null, settings, (IRequestParameters)null, memoryStreamFactory)
			{
				Node = new Node(new Uri("http://localhost:9200"))
			};

			var stream = new TrackDisposeStream();
			var response = ResponseBuilder.ToResponse<RootNodeInfoResponse>(requestData, null, statusCode, null, stream);

			memoryStreamFactory.Created.Count().Should().Be(disableDirectStreaming ? 1 : 0);
			stream.IsDisposed.Should().Be(true);

			stream = new TrackDisposeStream();
			var ct = new CancellationToken();
			response = await ResponseBuilder.ToResponseAsync<RootNodeInfoResponse>(requestData, null, statusCode, null, stream,
				cancellationToken: ct);
			memoryStreamFactory.Created.Count().Should().Be(disableDirectStreaming ? 2 : 0);
			stream.IsDisposed.Should().Be(true);
		}

		private class TrackDisposeStream : MemoryStream
		{
			public TrackDisposeStream() { }

			public TrackDisposeStream(byte[] bytes) : base(bytes) { }

			public TrackDisposeStream(byte[] bytes, int index, int count) : base(bytes, index, count) { }

			public bool IsDisposed { get; private set; }

			protected override void Dispose(bool disposing)
			{
				IsDisposed = true;
				base.Dispose(disposing);
			}
		}

		private class TrackMemoryStreamFactory : IMemoryStreamFactory
		{
			public IList<TrackDisposeStream> Created { get; } = new List<TrackDisposeStream>();

			public MemoryStream Create()
			{
				var stream = new TrackDisposeStream();
				Created.Add(stream);
				return stream;
			}

			public MemoryStream Create(byte[] bytes)
			{
				var stream = new TrackDisposeStream(bytes);
				Created.Add(stream);
				return stream;
			}

			public MemoryStream Create(byte[] bytes, int index, int count)
			{
				var stream = new TrackDisposeStream(bytes, index, count);
				Created.Add(stream);
				return stream;
			}
		}
	}
}
