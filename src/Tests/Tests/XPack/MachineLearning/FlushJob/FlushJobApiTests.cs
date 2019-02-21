using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.FlushJob
{
	public class FlushJobApiTests : MachineLearningIntegrationTestBase<IFlushJobResponse, IFlushJobRequest, FlushJobDescriptor, FlushJobRequest>
	{
		public FlushJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			advance_time = "2015-01-01T00:00:00+00:00",
			calc_interim = true
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<FlushJobDescriptor, IFlushJobRequest> Fluent => f => f
			.AdvanceTime(new DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero))
			.CalculateInterim();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override FlushJobRequest Initializer => new FlushJobRequest(CallIsolatedValue)
		{
			AdvanceTime = new DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero),
			CalculateInterim = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}/_flush";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) CloseJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.FlushJob(CallIsolatedValue, f),
			(client, f) => client.FlushJobAsync(CallIsolatedValue, f),
			(client, r) => client.FlushJob(r),
			(client, r) => client.FlushJobAsync(r)
		);

		protected override FlushJobDescriptor NewDescriptor() => new FlushJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IFlushJobResponse response) => response.Flushed.Should().BeTrue();
	}
}
