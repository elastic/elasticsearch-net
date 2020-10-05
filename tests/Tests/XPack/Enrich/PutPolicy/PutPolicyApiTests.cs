// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;
using static Nest.Infer;

namespace Tests.XPack.Enrich.PutPolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class PutPolicyApiTests
		: ApiTestBase<EnrichCluster, PutEnrichPolicyResponse, IPutEnrichPolicyRequest, PutEnrichPolicyDescriptor<Project>, PutEnrichPolicyRequest>
	{
		public PutPolicyApiTests(EnrichCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => PUT;

		protected override string UrlPath => $"/_enrich/policy/{CallIsolatedValue}";

		protected override object ExpectJson => new
		{
			match = new
			{
				indices = new[] { "project" },
				match_field = "name",
				enrich_fields = new[] { "description", "tags" }
			}
		};

		protected override PutEnrichPolicyRequest Initializer => new PutEnrichPolicyRequest(CallIsolatedValue)
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
		};

		protected override PutEnrichPolicyDescriptor<Project> NewDescriptor() => new PutEnrichPolicyDescriptor<Project>(CallIsolatedValue);

		protected override Func<PutEnrichPolicyDescriptor<Project>, IPutEnrichPolicyRequest> Fluent => f => f
			.Match(m => m
				.Indices(typeof(Project))
				.MatchField(mf => mf.Name)
				.EnrichFields(e => e
					.Field(ff => ff.Description)
					.Field(ff => ff.Tags)
				)
			);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.PutPolicy(CallIsolatedValue, f),
			(client, f) => client.Enrich.PutPolicyAsync(CallIsolatedValue, f),
			(client, r) => client.Enrich.PutPolicy(r),
			(client, r) => client.Enrich.PutPolicyAsync(r)
		);

	}
}
