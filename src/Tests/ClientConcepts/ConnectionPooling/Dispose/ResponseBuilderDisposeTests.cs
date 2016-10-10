using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.Exceptions
{
	public class ResponseBuilderDisposeTests
	{
		private IConnectionSettingsValues _settings = TestClient.CreateSettings(forceInMemory: true);
		private IConnectionSettingsValues _settingsDisableDirectStream =
			TestClient.CreateSettings(modifySettings: s=>s.DisableDirectStreaming(), forceInMemory: true);

		private class TrackDisposeStream : MemoryStream
		{
			public bool IsDisposed { get; private set; }
			protected override void Dispose(bool disposing)
			{
				this.IsDisposed = true;
				base.Dispose(disposing);
			}
		}

		private class TrackMemoryStreamFactory : IMemoryStreamFactory
		{
			public IList<TrackDisposeStream> Created { get; } = new List<TrackDisposeStream>();

			public MemoryStream Create()
			{
				var stream = new TrackDisposeStream();
				this.Created.Add(stream);
				return stream;
			}
		}

		[U] public async Task ResponseWithHttpStatusCode() => await ResponseBuilderAssert(false, r => r.StatusCode = 1);

		[U] public async Task ResponseBuilderWithNoHttpStatusCode() => await ResponseBuilderAssert(false);

		[U] public async Task ResponseWithHttpStatusCodeDisableDirectStreaming() =>
			await ResponseBuilderAssert(true, r => r.StatusCode = 1);

		[U] public async Task ResponseBuilderWithNoHttpStatusCodeDisableDirectStreaming() =>
			await ResponseBuilderAssert(true);

		private async Task ResponseBuilderAssert(bool disableDirectStreaming, Action<ResponseBuilder<RootNodeInfoResponse>> mutate = null)
		{
			var settings = disableDirectStreaming ? _settingsDisableDirectStream : _settings;
			var memoryStreamFactory = new TrackMemoryStreamFactory();
			var requestData = new RequestData(HttpMethod.GET, "/", null, settings, null, memoryStreamFactory)
			{
				Node = new Node(new Uri("http://localhost:9200"))
			};

			var responseBuilder = new ResponseBuilder<RootNodeInfoResponse>(requestData)
			{
			};
			mutate?.Invoke(responseBuilder);

			var stream = new TrackDisposeStream();
			responseBuilder.Stream = stream;

			var response = responseBuilder.ToResponse();
			memoryStreamFactory.Created.Count().Should().Be(disableDirectStreaming ? 1 : 0);
			if (disableDirectStreaming)
			{
				var memoryStream = memoryStreamFactory.Created[0];
				memoryStream.IsDisposed.Should().BeTrue();
			}
			stream.IsDisposed.Should().BeTrue();


			stream = new TrackDisposeStream();
			responseBuilder.Stream = stream;
			response = await responseBuilder.ToResponseAsync();
			memoryStreamFactory.Created.Count().Should().Be(disableDirectStreaming ? 2 : 0);
			if (disableDirectStreaming)
			{
				var memoryStream = memoryStreamFactory.Created[1];
				memoryStream.IsDisposed.Should().BeTrue();
			}
			stream.IsDisposed.Should().BeTrue();
		}

	}


}
