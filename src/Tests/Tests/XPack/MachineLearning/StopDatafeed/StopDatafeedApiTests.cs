using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.StopDatafeed
{
	public class StopDatafeedApiTests
		: MachineLearningIntegrationTestBase<IStopDatafeedResponse, IStopDatafeedRequest, StopDatafeedDescriptor, StopDatafeedRequest>
	{
		public StopDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<StopDatafeedDescriptor, IStopDatafeedRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override StopDatafeedRequest Initializer => new StopDatafeedRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed/_stop";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
				StartDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) CloseJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.StopDatafeed(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.StopDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.StopDatafeed(r),
			(client, r) => client.StopDatafeedAsync(r)
		);

		protected override StopDatafeedDescriptor NewDescriptor() => new StopDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IStopDatafeedResponse response) => response.Stopped.Should().BeTrue();
	}
}
