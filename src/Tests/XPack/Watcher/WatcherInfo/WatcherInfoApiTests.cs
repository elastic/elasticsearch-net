using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Watcher.WatcherInfo
{
	public class WatcherInfoApiTests : ApiIntegrationTestBase<XPackCluster, IWatcherInfoResponse, IWatcherInfoRequest, WatcherInfoDescriptor, WatcherInfoRequest>
	{
		public WatcherInfoApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.WatcherInfo(f),
			fluentAsync: (client, f) => client.WatcherInfoAsync(f),
			request: (client, r) => client.WatcherInfo(r),
			requestAsync: (client, r) => client.WatcherInfoAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => "/_watcher/";

		protected override bool SupportsDeserialization => true;

		protected override WatcherInfoRequest Initializer => new WatcherInfoRequest();

		protected override void ExpectResponse(IWatcherInfoResponse response)
		{
			response.Version.Should().NotBeNull();
			response.Version.Number.Should().NotBeNullOrEmpty();
			response.Version.BuildHash.Should().NotBeNullOrEmpty();
			response.Version.BuildTimestamp.Should().BeAfter(DateTimeOffset.MinValue);
			response.Tagline.Should().NotBeNullOrEmpty();
		}
	}
}
