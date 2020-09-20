// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.StopWatcher
{
	public class StopWatcherApiTests
		: ApiIntegrationTestBase<WatcherStateCluster, StopWatcherResponse, IStopWatcherRequest, StopWatcherDescriptor, StopWatcherRequest>
	{
		public StopWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<StopWatcherDescriptor, IStopWatcherRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override StopWatcherRequest Initializer => new StopWatcherRequest();

		protected override string UrlPath => "/_watcher/_stop";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Stop(f),
			(client, f) => client.Watcher.StopAsync(f),
			(client, r) => client.Watcher.Stop(r),
			(client, r) => client.Watcher.StopAsync(r)
		);

		protected override void ExpectResponse(StopWatcherResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
