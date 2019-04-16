using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetDatafeedStats
{
	public class GetDatafeedStatsApiTests
		: MachineLearningIntegrationTestBase<IGetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
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
			(client, f) => client.GetDatafeedStats(f),
			(client, f) => client.GetDatafeedStatsAsync(f),
			(client, r) => client.GetDatafeedStats(r),
			(client, r) => client.GetDatafeedStatsAsync(r)
		);

		protected override void ExpectResponse(IGetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Datafeeds.First().State.Should().Be(DatafeedState.Stopped);
		}
	}

	public class GetDatafeedStatsWithDatafeedIdApiTests
		: MachineLearningIntegrationTestBase<IGetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
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
			(client, f) => client.GetDatafeedStats(f),
			(client, f) => client.GetDatafeedStatsAsync(f),
			(client, r) => client.GetDatafeedStats(r),
			(client, r) => client.GetDatafeedStatsAsync(r)
		);

		protected override void ExpectResponse(IGetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Datafeeds.First().State.Should().Be(DatafeedState.Stopped);
		}
	}
}
