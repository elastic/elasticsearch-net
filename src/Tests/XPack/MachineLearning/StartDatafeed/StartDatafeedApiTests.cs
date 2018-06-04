using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.StartDatafeed
{
	public class StartDatafeedApiTests : MachineLearningIntegrationTestBase<IStartDatafeedResponse, IStartDatafeedRequest, StartDatafeedDescriptor, StartDatafeedRequest>
	{
		public StartDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
			fluent: (client, f) => client.StartDatafeed(CallIsolatedValue + "-datafeed", f),
			fluentAsync: (client, f) => client.StartDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			request: (client, r) => client.StartDatafeed(r),
			requestAsync: (client, r) => client.StartDatafeedAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/datafeeds/{CallIsolatedValue}-datafeed/_start";
		protected override bool SupportsDeserialization => true;
		protected override StartDatafeedDescriptor NewDescriptor() => new StartDatafeedDescriptor(CallIsolatedValue + "-datafeed");
		protected override object ExpectJson => null;
		protected override Func<StartDatafeedDescriptor, IStartDatafeedRequest> Fluent => f => f;
		protected override StartDatafeedRequest Initializer => new StartDatafeedRequest(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(IStartDatafeedResponse response)
		{
			response.Started.Should().BeTrue();
		}
	}
}
