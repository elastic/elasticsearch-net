// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using Tests.XPack.MachineLearning;

namespace Tests.Cat.CatJobs
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatJobsApiTests
		: MachineLearningIntegrationTestBase<CatResponse<CatJobsRecord>, ICatJobsRequest, CatJobsDescriptor, CatJobsRequest>
	{
		public CatJobsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
		}

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
