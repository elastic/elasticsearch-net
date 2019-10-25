using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.GetDatafeedStats
{
	public class GetDatafeedStatsApiTests
		: MachineLearningIntegrationTestBase<GetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_ml/datafeeds/_stats";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetDatafeedStats(f),
			(client, f) => client.MachineLearning.GetDatafeedStatsAsync(f),
			(client, r) => client.MachineLearning.GetDatafeedStats(r),
			(client, r) => client.MachineLearning.GetDatafeedStatsAsync(r)
		);

		protected override void ExpectResponse(GetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			var datafeedStats = response.Datafeeds.First();
			datafeedStats.State.Should().Be(DatafeedState.Stopped);

			if (Cluster.ClusterConfiguration.Version >= "7.4.0")
			{
				datafeedStats.TimingStats.Should().NotBeNull();
			}
		}
	}

	public class GetDatafeedStatsWithDatafeedIdApiTests
		: MachineLearningIntegrationTestBase<GetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsWithDatafeedIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f.DatafeedId(CallIsolatedValue + "-datafeed");
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override GetDatafeedStatsRequest Initializer => new GetDatafeedStatsRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed/_stats";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.GetDatafeedStats(f),
			(client, f) => client.MachineLearning.GetDatafeedStatsAsync(f),
			(client, r) => client.MachineLearning.GetDatafeedStats(r),
			(client, r) => client.MachineLearning.GetDatafeedStatsAsync(r)
		);

		protected override void ExpectResponse(GetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Datafeeds.First().State.Should().Be(DatafeedState.Stopped);
		}
	}
}
