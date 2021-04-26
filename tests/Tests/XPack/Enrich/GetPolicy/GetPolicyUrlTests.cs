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

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Enrich.GetPolicy
{
	public class GetEnrichPolicyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_enrich/policy/policy_name")
				.Fluent(c => c.Enrich.GetPolicy("policy_name", f => f))
				.Request(c => c.Enrich.GetPolicy(new GetEnrichPolicyRequest("policy_name")))
				.FluentAsync(c => c.Enrich.GetPolicyAsync("policy_name", f => f))
				.RequestAsync(c => c.Enrich.GetPolicyAsync(new GetEnrichPolicyRequest("policy_name")));

			await GET("/_enrich/policy/policy_name_1%2Cpolicy_name_2")
				.Fluent(c => c.Enrich.GetPolicy(new [] { "policy_name_1", "policy_name_2" }))
				.Request(c => c.Enrich.GetPolicy(new GetEnrichPolicyRequest(new [] { "policy_name_1", "policy_name_2" })))
				.FluentAsync(c => c.Enrich.GetPolicyAsync(new [] { "policy_name_1", "policy_name_2" }))
				.RequestAsync(c => c.Enrich.GetPolicyAsync(new GetEnrichPolicyRequest(new [] { "policy_name_1", "policy_name_2" })));

			await GET("/_enrich/policy")
				.Fluent(c => c.Enrich.GetPolicy())
				.Request(c => c.Enrich.GetPolicy(new GetEnrichPolicyRequest()))
				.FluentAsync(c => c.Enrich.GetPolicyAsync())
				.RequestAsync(c => c.Enrich.GetPolicyAsync(new GetEnrichPolicyRequest()));
		}
	}
}
