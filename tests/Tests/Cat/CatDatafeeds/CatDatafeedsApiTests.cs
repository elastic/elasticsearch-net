// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;
using Tests.XPack.MachineLearning;

namespace Tests.Cat.CatDatafeeds
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class CatDatafeedsApiTests
		: MachineLearningIntegrationTestBase<CatResponse<CatDatafeedsRecord>, ICatDatafeedsRequest, CatDatafeedsDescriptor,
			CatDatafeedsRequest>
	{
		public CatDatafeedsApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/ml/datafeeds";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Datafeeds(),
			(client, f) => client.Cat.DatafeedsAsync(),
			(client, r) => client.Cat.Datafeeds(r),
			(client, r) => client.Cat.DatafeedsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatDatafeedsRecord> response) => response.ShouldBeValid();
	}
}
