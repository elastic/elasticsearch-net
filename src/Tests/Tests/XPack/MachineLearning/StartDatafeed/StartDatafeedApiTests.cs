using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.StartDatafeed
{
	public class StartDatafeedApiTests
		: MachineLearningIntegrationTestBase<StartDatafeedResponse, IStartDatafeedRequest, StartDatafeedDescriptor, StartDatafeedRequest>
	{
		public StartDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<StartDatafeedDescriptor, IStartDatafeedRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override StartDatafeedRequest Initializer => new StartDatafeedRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed/_start";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				StopDatafeed(client, callUniqueValue.Value);
				CloseJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.StartDatafeed(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.MachineLearning.StartDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.MachineLearning.StartDatafeed(r),
			(client, r) => client.MachineLearning.StartDatafeedAsync(r)
		);

		protected override StartDatafeedDescriptor NewDescriptor() => new StartDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(StartDatafeedResponse response) => response.Started.Should().BeTrue();
	}
}
