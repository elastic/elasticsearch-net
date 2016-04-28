using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.XPack.Security.Role
{
	[Collection(IntegrationContext.Shield)]
	[SkipVersion("<2.3.0", "")]
	public class RoleCrudTests
		: CrudTestBase<IPutRoleResponse, IGetRoleResponse, IPutRoleResponse, IDeleteRoleResponse>
	{
		private string[] _roles = { "user" };
		public RoleCrudTests(ShieldCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string CreateRoleName(string s) => $"role-{s}";


		protected override LazyResponses Create() => Calls<PutRoleDescriptor, PutRoleRequest, IPutRoleRequest, IPutRoleResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.PutRole(CreateRoleName(s), f),
			fluentAsync: (s, c, f) => c.PutRoleAsync(CreateRoleName(s), f),
			request: (s, c, r) => c.PutRole(r),
			requestAsync: (s, c, r) => c.PutRoleAsync(r)
		);

		protected PutRoleRequest CreateInitializer(string role) => new PutRoleRequest(CreateRoleName(role))
		{
			Cluster = new[] { "all" },
			Indices = new List<IIndicesPrivileges>
			{
				new IndicesPrivileges
				{
					Fields = Fields<Project>(p=>p.Name).And<Project>(p=>p.Description),
					Names = Indices<Project>(),
					Privileges = new [] { "all" },
					Query = new MatchAllQuery()
				}
			}
		};
		protected IPutRoleRequest CreateFluent(string role, PutRoleDescriptor d) => d
			.Cluster("all")
			.Indices(i => i
				.Add<Project>(ii => ii
					.Fields(f => f
						.Field(p => p.Name)
						.Field(p => p.Description)
					)
					.Names(Indices<Project>())
					.Privileges("all")
					.Query(q => q.MatchAll())
				)
			);

		protected override LazyResponses Read() => Calls<GetRoleDescriptor, GetRoleRequest, IGetRoleRequest, IGetRoleResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetRole(f),
			fluentAsync: (s, c, f) => c.GetRoleAsync(f),
			request: (s, c, r) => c.GetRole(r),
			requestAsync: (s, c, r) => c.GetRoleAsync(r)
		);

		protected GetRoleRequest ReadInitializer(string role) => new GetRoleRequest(CreateRoleName(role));
		protected IGetRoleRequest ReadFluent(string role, GetRoleDescriptor d) => d.Name(CreateRoleName(role));

		protected override LazyResponses Update() => Calls<PutRoleDescriptor, PutRoleRequest, IPutRoleRequest, IPutRoleResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.PutRole(CreateRoleName(s), f),
			fluentAsync: (s, c, f) => c.PutRoleAsync(CreateRoleName(s), f),
			request: (s, c, r) => c.PutRole(r),
			requestAsync: (s, c, r) => c.PutRoleAsync(r)
		);

		protected PutRoleRequest UpdateInitializer(string role) => new PutRoleRequest(CreateRoleName(role))
		{
			Cluster = new[] { "all" },
			RunAs = new[] { "user" },
			Indices = new List<IIndicesPrivileges>
			{
				new IndicesPrivileges
				{
					Fields = Fields<Project>(p=>p.Name).And<Project>(p=>p.Description),
					Names = Indices<Project>(),
					Privileges = new [] { "all" },
					Query = new MatchAllQuery()
				}
			}
		};
		protected IPutRoleRequest UpdateFluent(string role, PutRoleDescriptor d) => d
			.RunAs("user")
			.Cluster("all")
			.Indices(i => i
				.Add<Project>(ii => ii
					.Fields(f => f
						.Field(p => p.Name)
						.Field(p => p.Description)
					)
					.Names(Indices<Project>())
					.Privileges("all")
					.Query(q => q.MatchAll())
				)
			);

		protected override LazyResponses Delete() => Calls<DeleteRoleDescriptor, DeleteRoleRequest, IDeleteRoleRequest, IDeleteRoleResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteRole(CreateRoleName(s), f),
			fluentAsync: (s, c, f) => c.DeleteRoleAsync(CreateRoleName(s), f),
			request: (s, c, r) => c.DeleteRole(r),
			requestAsync: (s, c, r) => c.DeleteRoleAsync(r)
		);

		protected DeleteRoleRequest DeleteInitializer(string role) => new DeleteRoleRequest(CreateRoleName(role));
		protected IDeleteRoleRequest DeleteFluent(string role, DeleteRoleDescriptor d) => d;

		protected override void ExpectAfterCreate(IGetRoleResponse response)
		{
			response.Roles.Should().NotBeEmpty();
			var role = response.Roles.First().Value;
			role.Cluster.Should().NotBeNullOrEmpty().And.Contain("all");
			role.RunAs.Should().BeNullOrEmpty();
			role.Indices.Should().NotBeNullOrEmpty().And.HaveCount(1);

			var indexPrivilege = role.Indices.First();
			indexPrivilege.Names.Should().NotBeNull().And.Be(Infer.Indices("project"));
			indexPrivilege.Fields.Should().NotBeNull().And.HaveCount(2);
			indexPrivilege.Privileges.Should().NotBeNull().And.Contain("all");
			indexPrivilege.Query.Should().NotBeNull();
			var q = indexPrivilege.Query as IQueryContainer;
			q.MatchAll.Should().NotBeNull();
		}
		protected override void ExpectAfterUpdate(IGetRoleResponse response)
		{
			response.Roles.Should().NotBeEmpty();
			var role = response.Roles.First().Value;
			role.Cluster.Should().NotBeNullOrEmpty().And.Contain("all");
			role.RunAs.Should().NotBeNullOrEmpty();
			role.Indices.Should().NotBeNullOrEmpty().And.HaveCount(1);

			var indexPrivilege = role.Indices.First();
			indexPrivilege.Names.Should().NotBeNull().And.Be(Infer.Indices("project"));
			indexPrivilege.Fields.Should().NotBeNull().And.HaveCount(2);
			indexPrivilege.Privileges.Should().NotBeNull().And.Contain("all");
			indexPrivilege.Query.Should().NotBeNull();
			var q = indexPrivilege.Query as IQueryContainer;
			q.MatchAll.Should().NotBeNull();
		}
	}
}
