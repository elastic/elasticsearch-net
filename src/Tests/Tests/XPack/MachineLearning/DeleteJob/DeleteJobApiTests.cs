using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteJob
{
	public class DeleteJobApiTests : MachineLearningIntegrationTestBase<IDeleteJobResponse, IDeleteJobRequest, DeleteJobDescriptor, DeleteJobRequest>
	{
		public DeleteJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteJobDescriptor, IDeleteJobRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteJobRequest Initializer => new DeleteJobRequest(CallIsolatedValue);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteJob(CallIsolatedValue, f),
			(client, f) => client.DeleteJobAsync(CallIsolatedValue, f),
			(client, r) => client.DeleteJob(r),
			(client, r) => client.DeleteJobAsync(r)
		);

		protected override DeleteJobDescriptor NewDescriptor() => new DeleteJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteJobResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
