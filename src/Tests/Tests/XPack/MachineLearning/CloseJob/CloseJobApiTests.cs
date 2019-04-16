using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.CloseJob
{
	public class CloseJobApiTests : MachineLearningIntegrationTestBase<ICloseJobResponse, ICloseJobRequest, CloseJobDescriptor, CloseJobRequest>
	{
		public CloseJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<CloseJobDescriptor, ICloseJobRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override CloseJobRequest Initializer => new CloseJobRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/_close";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CloseJob(CallIsolatedValue, f),
			(client, f) => client.CloseJobAsync(CallIsolatedValue, f),
			(client, r) => client.CloseJob(r),
			(client, r) => client.CloseJobAsync(r)
		);

		protected override CloseJobDescriptor NewDescriptor() => new CloseJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(ICloseJobResponse response) => response.Closed.Should().BeTrue();
	}
}
