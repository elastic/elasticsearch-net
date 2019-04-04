using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.MachineLearning.DeleteFilter
{
	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class DeleteFilterApiTests : MachineLearningIntegrationTestBase<IDeleteFilterResponse, IDeleteFilterRequest, DeleteFilterDescriptor, DeleteFilterRequest>
	{
		public DeleteFilterApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutFilter(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteFilterDescriptor, IDeleteFilterRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteFilterRequest Initializer => new DeleteFilterRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_xpack/ml/filters/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteFilter(CallIsolatedValue, f),
			(client, f) => client.DeleteFilterAsync(CallIsolatedValue, f),
			(client, r) => client.DeleteFilter(r),
			(client, r) => client.DeleteFilterAsync(r)
		);

		protected override DeleteFilterDescriptor NewDescriptor() => new DeleteFilterDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IDeleteFilterResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
