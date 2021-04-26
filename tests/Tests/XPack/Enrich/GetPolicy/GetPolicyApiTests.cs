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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Transport.HttpMethod;

namespace Tests.XPack.Enrich.GetPolicy
{
	[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
	public class GetPolicyApiTests
		: ApiTestBase<EnrichCluster, GetEnrichPolicyResponse, IGetEnrichPolicyRequest, GetEnrichPolicyDescriptor, GetEnrichPolicyRequest>
	{
		private static readonly string PolicyName = "example_policy";

		public GetPolicyApiTests(EnrichCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => GET;

		protected override string UrlPath => $"/_enrich/policy/{PolicyName}";

		protected override GetEnrichPolicyRequest Initializer => new GetEnrichPolicyRequest(PolicyName);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Enrich.GetPolicy(PolicyName, f),
			(client, f) => client.Enrich.GetPolicyAsync(PolicyName, f),
			(client, r) => client.Enrich.GetPolicy(r),
			(client, r) => client.Enrich.GetPolicyAsync(r)
		);
	}
}
