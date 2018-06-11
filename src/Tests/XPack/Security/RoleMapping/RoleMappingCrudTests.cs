using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.XPack.Security.RoleMapping
{
	[SkipVersion("<5.5.0", "Does not exist in earlier versions")]
	public class RoleMappingCrudTests
		: CrudTestBase<XPackCluster, IPutRoleMappingResponse, IGetRoleMappingResponse, IPutRoleMappingResponse, IDeleteRoleMappingResponse>
	{
		private readonly string _dn = "*,ou=admin,dc=example,dc=com";
		private readonly string _username = "mpdreamz";
		private readonly string _realm = "some_realm";
		private readonly Tuple<string, string> _metadata = Tuple.Create("a", "b");
		private readonly string[] _groups = {"group1", "group2"};

		public RoleMappingCrudTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private static string CreateRoleMappingName(string s) => $"role-mapping-{s}";

		protected override LazyResponses Create() => Calls<PutRoleMappingDescriptor, PutRoleMappingRequest, IPutRoleMappingRequest, IPutRoleMappingResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.PutRoleMapping(CreateRoleMappingName(s), f),
			fluentAsync: (s, c, f) => c.PutRoleMappingAsync(CreateRoleMappingName(s), f),
			request: (s, c, r) => c.PutRoleMapping(r),
			requestAsync: (s, c, r) => c.PutRoleMappingAsync(r)
		);

		protected PutRoleMappingRequest CreateInitializer(string role) => new PutRoleMappingRequest(CreateRoleMappingName(role))
		{
			Enabled = false,
			Roles = new [] { "admin"},
			Metadata = new Dictionary<string, object>
			{
				{"x", "y"},
				{"z", null}
			},
			Rules =
				(new DistinguishedNameRule(_dn) | new UsernameRule(_username) | new RealmRule(_realm))
				& new MetadataRule(_metadata.Item1, _metadata.Item2)
				& !new GroupsRule(_groups)
		};
		protected IPutRoleMappingRequest CreateFluent(string role, PutRoleMappingDescriptor d) => d
			.Enabled(false)
			.Roles("admin")
			.Metadata(f=>f.Add("x", "y").Add("z", null))
			.Rules(r => r
				.All(all => all
					.Any(any => any
						.DistinguishedName(_dn)
						.Username(_username)
						.Realm(_realm)
					)
					.Metadata(_metadata.Item1, _metadata.Item2)
					.Except(e=>e.Groups(_groups))
				)
			);

		protected override LazyResponses Read() => Calls<GetRoleMappingDescriptor, GetRoleMappingRequest, IGetRoleMappingRequest, IGetRoleMappingResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetRoleMapping(f),
			fluentAsync: (s, c, f) => c.GetRoleMappingAsync(f),
			request: (s, c, r) => c.GetRoleMapping(r),
			requestAsync: (s, c, r) => c.GetRoleMappingAsync(r)
		);

		protected GetRoleMappingRequest ReadInitializer(string role) => new GetRoleMappingRequest(CreateRoleMappingName(role));
		protected IGetRoleMappingRequest ReadFluent(string role, GetRoleMappingDescriptor d) => d.Name(CreateRoleMappingName(role));

		protected override LazyResponses Update() => Calls<PutRoleMappingDescriptor, PutRoleMappingRequest, IPutRoleMappingRequest, IPutRoleMappingResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.PutRoleMapping(CreateRoleMappingName(s), f),
			fluentAsync: (s, c, f) => c.PutRoleMappingAsync(CreateRoleMappingName(s), f),
			request: (s, c, r) => c.PutRoleMapping(r),
			requestAsync: (s, c, r) => c.PutRoleMappingAsync(r)
		);

		protected PutRoleMappingRequest UpdateInitializer(string role) => new PutRoleMappingRequest(CreateRoleMappingName(role))
		{
			Enabled = true,
			Roles = new [] { "admin", "user" },
			Metadata = new Dictionary<string, object>
			{
				{"x", "y"},
				{"z", "zz"}
			},
			Rules =
				(new DistinguishedNameRule(_dn) | new UsernameRule(_username) | new RealmRule(_realm))
				& new MetadataRule(_metadata.Item1, _metadata.Item2)
				& !new GroupsRule(_groups)
		};
		protected IPutRoleMappingRequest UpdateFluent(string role, PutRoleMappingDescriptor d) => d
			.Enabled()
			.Roles("admin", "user")
			.Metadata(f=>f.Add("x", "y").Add("z", "zz"))
			.Rules(r => r
				.All(all => all
					.Any(any => any
						.DistinguishedName(_dn)
						.Username(_username)
						.Realm(_realm)
					)
					.Metadata(_metadata.Item1, _metadata.Item2)
					.Except(e=>e.Groups(_groups))
				)
			);

		protected override LazyResponses Delete() => Calls<DeleteRoleMappingDescriptor, DeleteRoleMappingRequest, IDeleteRoleMappingRequest, IDeleteRoleMappingResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteRoleMapping(CreateRoleMappingName(s), f),
			fluentAsync: (s, c, f) => c.DeleteRoleMappingAsync(CreateRoleMappingName(s), f),
			request: (s, c, r) => c.DeleteRoleMapping(r),
			requestAsync: (s, c, r) => c.DeleteRoleMappingAsync(r)
		);

		protected DeleteRoleMappingRequest DeleteInitializer(string role) => new DeleteRoleMappingRequest(CreateRoleMappingName(role));
		protected IDeleteRoleMappingRequest DeleteFluent(string role, DeleteRoleMappingDescriptor d) => d;

		protected override void ExpectAfterCreate(IGetRoleMappingResponse response)
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
		protected override void ExpectAfterUpdate(IGetRoleMappingResponse response)
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

		protected override void ExpectDeleteNotFoundResponse(IDeleteRoleMappingResponse response)
		{
			response.Found.Should().BeFalse();
			response.ServerError.Should().BeNull();
		}
	}
}
