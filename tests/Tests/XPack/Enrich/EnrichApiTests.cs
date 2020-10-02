// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.XPack.Enrich
{
	[SkipVersion("<7.5.0", "Introduced in 7.50")]
	public class EnrichApiTests : CoordinatedIntegrationTestBase<EnrichCluster>
	{
		private const string PutPolicyStep = nameof(PutPolicyStep);
		private const string GetPolicyStep = nameof(GetPolicyStep);
		private const string ExecutePolicyStep = nameof(ExecutePolicyStep);
		private const string StatsStep = nameof(StatsStep);
		private const string DeletePolicyStep = nameof(DeletePolicyStep);

		public EnrichApiTests(EnrichCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutPolicyStep, u =>
					u.Calls<PutEnrichPolicyDescriptor<Project>, PutEnrichPolicyRequest, IPutEnrichPolicyRequest, PutEnrichPolicyResponse>(
						v => new PutEnrichPolicyRequest(v)
						{
							Match = new EnrichPolicy
							{
								Indices = typeof(Project),
								MatchField = Field<Project>(f => f.Name),
								EnrichFields = Fields<Project>(
									f => f.Description,
									f => f.Tags
								)
							}
						},
						(v, d) => d
							.Match(m => m
								.Indices(typeof(Project))
								.MatchField(f => f.Name)
								.EnrichFields(f => f
									.Field(ff => ff.Description)
									.Field(ff => ff.Tags)
								)
							),
						(v, c, f) => c.Enrich.PutPolicy(v, f),
						(v, c, f) => c.Enrich.PutPolicyAsync(v, f),
						(v, c, r) => c.Enrich.PutPolicy(r),
						(v, c, r) => c.Enrich.PutPolicyAsync(r)
					)
			},
			{
				GetPolicyStep, u =>
					u.Calls<GetEnrichPolicyDescriptor, GetEnrichPolicyRequest, IGetEnrichPolicyRequest, GetEnrichPolicyResponse>(
						v => new GetEnrichPolicyRequest(v),
						(v, d) => d,
						(v, c, f) => c.Enrich.GetPolicy(v, f),
						(v, c, f) => c.Enrich.GetPolicyAsync(v, f),
						(v, c, r) => c.Enrich.GetPolicy(r),
						(v, c, r) => c.Enrich.GetPolicyAsync(r)
					)
			},
			{
				ExecutePolicyStep, u =>
					u.Calls<ExecuteEnrichPolicyDescriptor, ExecuteEnrichPolicyRequest, IExecuteEnrichPolicyRequest, ExecuteEnrichPolicyResponse>(
						v => new ExecuteEnrichPolicyRequest(v),
						(v, d) => d,
						(v, c, f) => c.Enrich.ExecutePolicy(v, f),
						(v, c, f) => c.Enrich.ExecutePolicyAsync(v, f),
						(v, c, r) => c.Enrich.ExecutePolicy(r),
						(v, c, r) => c.Enrich.ExecutePolicyAsync(r)
					)
			},
			{
				StatsStep, u =>
					u.Calls<EnrichStatsDescriptor, EnrichStatsRequest, IEnrichStatsRequest, EnrichStatsResponse>(
						v => new EnrichStatsRequest(),
						(v, d) => d,
						(v, c, f) => c.Enrich.Stats(f),
						(v, c, f) => c.Enrich.StatsAsync(f),
						(v, c, r) => c.Enrich.Stats(r),
						(v, c, r) => c.Enrich.StatsAsync(r)
					)
			},
			{
				DeletePolicyStep, u =>
					u.Calls<DeleteEnrichPolicyDescriptor, DeleteEnrichPolicyRequest, IDeleteEnrichPolicyRequest, DeleteEnrichPolicyResponse>(
						v => new DeleteEnrichPolicyRequest(v),
						(v, d) => d,
						(v, c, f) => c.Enrich.DeletePolicy(v, f),
						(v, c, f) => c.Enrich.DeletePolicyAsync(v, f),
						(v, c, r) => c.Enrich.DeletePolicy(r),
						(v, c, r) => c.Enrich.DeletePolicyAsync(r)
					)
			},
		}) { }

		[I] public async Task PutEnrichPolicyResponse() => await Assert<PutEnrichPolicyResponse>(PutPolicyStep, (v, r) =>
		{
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task GetEnrichPolicyResponse() => await Assert<GetEnrichPolicyResponse>(GetPolicyStep, (v, r) =>
		{
			r.Policies.Should().HaveCount(1);
			var policyConfig = r.Policies.First().Config;

			policyConfig.Match.Should().NotBeNull();
			policyConfig.Match.Name.Should().Be(v);
			policyConfig.Match.Indices.Should().Be((Nest.Indices)"project");
			policyConfig.Match.MatchField.Should().Be("name");
			policyConfig.Match.EnrichFields.Should().HaveCount(2).And.Contain(new Field[] { "description", "tags" });

		});

		[I] public async Task ExecuteEnrichPolicyResponse() => await Assert<ExecuteEnrichPolicyResponse>(ExecutePolicyStep, (v, r) =>
		{
			r.Status.Phase.Should().Be(EnrichPolicyPhase.Complete);
		});

		[I] public async Task EnrichStatsResponse() => await Assert<EnrichStatsResponse>(StatsStep, (v, r) =>
		{
			r.ExecutingPolicies.Should().NotBeNull();
			r.CoordinatorStats.Should().NotBeNull();
		});

		[I] public async Task DeleteEnrichPolicyResponse() => await Assert<DeleteEnrichPolicyResponse>(DeletePolicyStep, (v, r) =>
		{
			r.Acknowledged.Should().BeTrue();
		});
	}
}
