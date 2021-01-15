// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
