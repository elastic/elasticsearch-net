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
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.RoleMapping
{
	[SkipVersion("<5.5.0", "Does not exist in earlier versions")]
	public class DistinguishedNamesRoleMappingsTests
		: ApiTestBase<XPackCluster, PutRoleMappingResponse, IPutRoleMappingRequest, PutRoleMappingDescriptor, PutRoleMappingRequest>
	{
		public DistinguishedNamesRoleMappingsTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => "/_security/role_mapping/name";
		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			enabled = true,
			roles = new[] { "user_role" },
			rules = new
			{
				any = new object[]
				{
					new
					{
						field = new {
							dn = new [] {
								"a",
								"b"
							}
						}
					}
				}
			}
		};

		protected override PutRoleMappingRequest Initializer { get; } = new("name")
		{
			Enabled = true,
			Roles = new List<string> { "user_role" },
			Rules = new AnyRoleMappingRule(new DistinguishedNameRule(new List<string>
			{
				"a", "b"
			}))
		};

		protected override PutRoleMappingDescriptor NewDescriptor() => new("name");

		protected override Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> Fluent =>
			d => d.Enabled().Roles("user_role").Rules(r => r.Any(a => a.DistinguishedName(new List<string> { "a", "b" })));

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.PutRoleMapping("name", f),
			(client, f) => client.Security.PutRoleMappingAsync("name", f),
			(client, r) => client.Security.PutRoleMapping(r),
			(client, r) => client.Security.PutRoleMappingAsync(r));
	}
}
