// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.RoleMapping
{
	[SkipVersion("<5.5.0", "Does not exist in earlier versions")]
	public class RoleMappingCrudTests
		: CrudTestBase<XPackCluster, PutRoleMappingResponse, GetRoleMappingResponse, PutRoleMappingResponse, DeleteRoleMappingResponse>
	{
		private readonly string _dn = "*,ou=admin,dc=example,dc=com";
		private readonly string[] _groups = { "group1", "group2" };
		private readonly Tuple<string, string> _metadata = Tuple.Create("a", "b");
		private readonly string _realm = "some_realm";
		private readonly string _username = "mpdreamz";

		public RoleMappingCrudTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private static string CreateRoleMappingName(string s) => $"role-mapping-{s}";

		protected override LazyResponses Create() =>
			Calls<PutRoleMappingDescriptor, PutRoleMappingRequest, IPutRoleMappingRequest, PutRoleMappingResponse>(
				CreateInitializer,
				CreateFluent,
				(s, c, f) => c.Security.PutRoleMapping(CreateRoleMappingName(s), f),
				(s, c, f) => c.Security.PutRoleMappingAsync(CreateRoleMappingName(s), f),
				(s, c, r) => c.Security.PutRoleMapping(r),
				(s, c, r) => c.Security.PutRoleMappingAsync(r)
			);

		protected PutRoleMappingRequest CreateInitializer(string role) => new PutRoleMappingRequest(CreateRoleMappingName(role))
		{
			Enabled = false,
			Roles = new[] { "admin" },
			Metadata = new Dictionary<string, object>
			{
				{ "x", "y" },
				{ "z", null }
			},
			Rules =
				(new DistinguishedNameRule(_dn) | new UsernameRule(_username) | new RealmRule(_realm))
				& new MetadataRule(_metadata.Item1, _metadata.Item2)
				& !new GroupsRule(_groups)
		};

		protected IPutRoleMappingRequest CreateFluent(string role, PutRoleMappingDescriptor d) => d
			.Enabled(false)
			.Roles("admin")
			.Metadata(f => f.Add("x", "y").Add("z", null))
			.Rules(r => r
				.All(all => all
					.Any(any => any
						.DistinguishedName(_dn)
						.Username(_username)
						.Realm(_realm)
					)
					.Metadata(_metadata.Item1, _metadata.Item2)
					.Except(e => e.Groups(_groups))
				)
			);

		protected override LazyResponses Read() =>
			Calls<GetRoleMappingDescriptor, GetRoleMappingRequest, IGetRoleMappingRequest, GetRoleMappingResponse>(
				ReadInitializer,
				ReadFluent,
				(s, c, f) => c.Security.GetRoleMapping(CreateRoleMappingName(s), f),
				(s, c, f) => c.Security.GetRoleMappingAsync(CreateRoleMappingName(s), f),
				(s, c, r) => c.Security.GetRoleMapping(r),
				(s, c, r) => c.Security.GetRoleMappingAsync(r)
			);

		protected GetRoleMappingRequest ReadInitializer(string role) => new GetRoleMappingRequest(CreateRoleMappingName(role));

		protected IGetRoleMappingRequest ReadFluent(string role, GetRoleMappingDescriptor d) => d;

		protected override LazyResponses Update() =>
			Calls<PutRoleMappingDescriptor, PutRoleMappingRequest, IPutRoleMappingRequest, PutRoleMappingResponse>(
				UpdateInitializer,
				UpdateFluent,
				(s, c, f) => c.Security.PutRoleMapping(CreateRoleMappingName(s), f),
				(s, c, f) => c.Security.PutRoleMappingAsync(CreateRoleMappingName(s), f),
				(s, c, r) => c.Security.PutRoleMapping(r),
				(s, c, r) => c.Security.PutRoleMappingAsync(r)
			);

		protected PutRoleMappingRequest UpdateInitializer(string role) => new PutRoleMappingRequest(CreateRoleMappingName(role))
		{
			Enabled = true,
			Roles = new[] { "admin", "user" },
			Metadata = new Dictionary<string, object>
			{
				{ "x", "y" },
				{ "z", "zz" }
			},
			Rules =
				(new DistinguishedNameRule(_dn) | new UsernameRule(_username) | new RealmRule(_realm))
				& new MetadataRule(_metadata.Item1, _metadata.Item2)
				& !new GroupsRule(_groups)
		};

		protected IPutRoleMappingRequest UpdateFluent(string role, PutRoleMappingDescriptor d) => d
			.Enabled()
			.Roles("admin", "user")
			.Metadata(f => f.Add("x", "y").Add("z", "zz"))
			.Rules(r => r
				.All(all => all
					.Any(any => any
						.DistinguishedName(_dn)
						.Username(_username)
						.Realm(_realm)
					)
					.Metadata(_metadata.Item1, _metadata.Item2)
					.Except(e => e.Groups(_groups))
				)
			);

		protected override LazyResponses Delete() =>
			Calls<DeleteRoleMappingDescriptor, DeleteRoleMappingRequest, IDeleteRoleMappingRequest, DeleteRoleMappingResponse>(
				DeleteInitializer,
				DeleteFluent,
				(s, c, f) => c.Security.DeleteRoleMapping(CreateRoleMappingName(s), f),
				(s, c, f) => c.Security.DeleteRoleMappingAsync(CreateRoleMappingName(s), f),
				(s, c, r) => c.Security.DeleteRoleMapping(r),
				(s, c, r) => c.Security.DeleteRoleMappingAsync(r)
			);

		protected DeleteRoleMappingRequest DeleteInitializer(string role) => new DeleteRoleMappingRequest(CreateRoleMappingName(role));

		protected IDeleteRoleMappingRequest DeleteFluent(string role, DeleteRoleMappingDescriptor d) => d;

		protected override void ExpectAfterCreate(GetRoleMappingResponse response)
		{
			response.RoleMappings.Should().NotBeEmpty();
			var mapping = response.RoleMappings.First().Value;

			mapping.Enabled.Should().BeFalse();
			mapping.Roles.Should().BeEquivalentTo("admin");
			mapping.Metadata.Should().HaveCount(2).And.ContainKeys("x", "z");
			mapping.Rules.Should().NotBeNull();

			var allMapping = mapping.Rules as AllRoleMappingRule;
			allMapping.Should().NotBeNull("expect to get back an all role mapping rule");
		}

		protected override void ExpectAfterUpdate(GetRoleMappingResponse response)
		{
			response.RoleMappings.Should().NotBeEmpty();
			var mapping = response.RoleMappings.First().Value;

			mapping.Enabled.Should().BeTrue();
			mapping.Roles.Should().BeEquivalentTo("admin", "user");
			mapping.Metadata.Should().HaveCount(2).And.ContainKeys("x", "z");
			mapping.Rules.Should().NotBeNull();

			var allMapping = mapping.Rules as AllRoleMappingRule;
			allMapping.Should().NotBeNull("expect to get back an all role mapping rule");
		}

		protected override void ExpectDeleteNotFoundResponse(DeleteRoleMappingResponse response)
		{
			response.Found.Should().BeFalse();
			response.ServerError.Should().BeNull();
		}
	}
}
