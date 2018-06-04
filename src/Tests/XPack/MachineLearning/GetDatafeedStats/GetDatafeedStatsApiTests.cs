using System;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.GetDatafeedStats
{
	public class GetDatafeedStatsApiTests : MachineLearningIntegrationTestBase<IGetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeedStats(f),
			fluentAsync: (client, f) => client.GetDatafeedStatsAsync(f),
			request: (client, r) => client.GetDatafeedStats(r),
			requestAsync: (client, r) => client.GetDatafeedStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_xpack/ml/datafeeds/_stats";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f;

		protected override void ExpectResponse(IGetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Datafeeds.First().State.Should().Be(DatafeedState.Stopped);
		}
	}

	public class GetDatafeedStatsWithDatafeedIdApiTests : MachineLearningIntegrationTestBase<IGetDatafeedStatsResponse, IGetDatafeedStatsRequest, GetDatafeedStatsDescriptor, GetDatafeedStatsRequest>
	{
		public GetDatafeedStatsWithDatafeedIdApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetDatafeedStats(f),
			fluentAsync: (client, f) => client.GetDatafeedStatsAsync(f),
			request: (client, r) => client.GetDatafeedStats(r),
			requestAsync: (client, r) => client.GetDatafeedStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed/_stats";
		protected override bool SupportsDeserialization => true;
		protected override object ExpectJson => null;
		protected override Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> Fluent => f => f.DatafeedId(CallIsolatedValue + "-datafeed");
		protected override GetDatafeedStatsRequest Initializer => new GetDatafeedStatsRequest(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IGetDatafeedStatsResponse response)
		{
			response.ShouldBeValid();
			response.Count.Should().BeGreaterOrEqualTo(1);
			response.Datafeeds.First().State.Should().Be(DatafeedState.Stopped);
		}
	}
}
