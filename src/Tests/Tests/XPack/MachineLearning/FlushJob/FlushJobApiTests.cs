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
			foreach (var callUniqueValue in values)
			{
				CloseJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.FlushJob(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.FlushJobAsync(CallIsolatedValue, f),
			request: (client, r) => client.FlushJob(r),
			requestAsync: (client, r) => client.FlushJobAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"_xpack/ml/anomaly_detectors/{CallIsolatedValue}/_flush";
		protected override bool SupportsDeserialization => false;
		protected override FlushJobDescriptor NewDescriptor() => new FlushJobDescriptor(CallIsolatedValue);

		protected override object ExpectJson => new
		{
			advance_time = "2015-01-01T00:00:00+00:00",
			calc_interim = true
		};

		protected override Func<FlushJobDescriptor, IFlushJobRequest> Fluent => f => f
			.AdvanceTime(new DateTimeOffset(2015, 1 , 1, 0, 0, 0, TimeSpan.Zero))
			.CalculateInterim();

		protected override FlushJobRequest Initializer => new FlushJobRequest(CallIsolatedValue)
		{
			AdvanceTime = new DateTimeOffset(2015, 1 , 1, 0, 0, 0, TimeSpan.Zero),
			CalculateInterim = true
		};

		protected override void ExpectResponse(IFlushJobResponse response)
		{
			response.Flushed.Should().BeTrue();
		}
	}
}
