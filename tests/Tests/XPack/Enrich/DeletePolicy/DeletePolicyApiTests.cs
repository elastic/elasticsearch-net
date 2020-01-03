using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.Enrich.DeletePolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class DeletePolicyApiTests
		: ApiIntegrationTestBase<XPackCluster, DeleteEnrichPolicyResponse, IDeleteEnrichPolicyRequest, DeleteEnrichPolicyDescriptor, DeleteEnrichPolicyRequest>
	{
		public DeletePolicyApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => DELETE;

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putPolicyResponse = client.Enrich.PutPolicy<Project>(callUniqueValue.Value, p => p
					.Match(m => m
						.Indices(typeof(Project))
						.MatchField(f => f.Name)
						.EnrichFields(f => f
							.Field(ff => ff.Description)
							.Field(ff => ff.Tags)
						)
					)
				);

				if (!putPolicyResponse.IsValid)
					throw new Exception($"Failure setting up integration test: {putPolicyResponse.DebugInformation}");
			}
		}

		protected override DeleteEnrichPolicyRequest Initializer => new DeleteEnrichPolicyRequest(CallIsolatedValue);

		protected override string UrlPath => $"/_enrich/policy/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.DeletePolicy(CallIsolatedValue, f),
			(client, f) => client.Enrich.DeletePolicyAsync(CallIsolatedValue, f),
			(client, r) => client.Enrich.DeletePolicy(r),
			(client, r) => client.Enrich.DeletePolicyAsync(r)
		);

		protected override void ExpectResponse(DeleteEnrichPolicyResponse response) =>
			response.Acknowledged.Should().BeTrue();
	}
}
