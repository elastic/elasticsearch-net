using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatJobs
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatJobsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatJobsRecord>, ICatJobsRequest, CatJobsDescriptor,
			CatJobsRequest>
	{
		public CatJobsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "_cat/ml/anomaly_detectors";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Jobs(),
			(client, f) => client.Cat.JobsAsync(),
			(client, r) => client.Cat.Jobs(r),
			(client, r) => client.Cat.JobsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatJobsRecord> response) => response.ShouldBeValid();
	}
}
