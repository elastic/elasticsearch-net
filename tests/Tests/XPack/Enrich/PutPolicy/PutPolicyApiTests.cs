/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
