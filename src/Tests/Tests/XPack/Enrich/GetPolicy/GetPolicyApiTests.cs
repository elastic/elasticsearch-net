using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.Enrich.GetPolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class GetPolicyApiTests
		: ApiIntegrationTestBase<XPackCluster, GetEnrichPolicyResponse, IGetEnrichPolicyRequest, GetEnrichPolicyDescriptor, GetEnrichPolicyRequest>
	{
		private static readonly string PolicyName = "example_policy";

		public GetPolicyApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override GetEnrichPolicyRequest Initializer => new GetEnrichPolicyRequest(PolicyName);

		protected override string UrlPath => $"/_enrich/policy/{PolicyName}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var putPolicyResponse = client.Enrich.PutPolicy<Project>(PolicyName, p => p
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

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.GetPolicy(PolicyName, f),
			(client, f) => client.Enrich.GetPolicyAsync(PolicyName, f),
			(client, r) => client.Enrich.GetPolicy(r),
			(client, r) => client.Enrich.GetPolicyAsync(r)
		);

		protected override void ExpectResponse(GetEnrichPolicyResponse response)
		{
			response.Policies.Should().HaveCount(1);
			var policyConfig = response.Policies.First().Config;

			policyConfig.Match.Should().NotBeNull();
			policyConfig.Match.Name.Should().Be(PolicyName);
			policyConfig.Match.Indices.Should().Be((Nest.Indices)"project");
			policyConfig.Match.MatchField.Should().Be("name");
			policyConfig.Match.EnrichFields.Should().HaveCount(2).And.Contain(new Field[] { "description", "tags" });
		}
	}
}
