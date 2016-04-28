using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.XPack.Security.Role.PutRole
{
	[Collection(IntegrationContext.Shield)]
	[SkipVersion("<2.3.0", "")]
	public class PutRoleApiTests : ApiIntegrationTestBase<IPutRoleResponse, IPutRoleRequest, PutRoleDescriptor, PutRoleRequest>
	{
		public PutRoleApiTests(ShieldCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutRole(this.Role, f),
			fluentAsync: (client, f) => client.PutRoleAsync(this.Role, f),
			request: (client, r) => client.PutRole(r),
			requestAsync: (client, r) => client.PutRoleAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"/_shield/role/{this.Role}";

		protected override bool SupportsDeserialization => false;

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Role => $"role-{CallIsolatedValue}";

		protected override object ExpectJson => new
		{
			cluster = new [] { "all" },
			run_as = new [] { "user" },
			indices = new [] {
				new {
					names = new [] { "project" },
					privileges = new [] { "all" },
					fields = new [] { "name", "description" },
					query = new { match_all = new {} }
				}
			}
		};

		protected override PutRoleRequest Initializer => new PutRoleRequest(this.Role)
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

		protected override PutRoleDescriptor NewDescriptor() => new PutRoleDescriptor(this.Role);

		protected override Func<PutRoleDescriptor, IPutRoleRequest> Fluent => d => d
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

		protected override void ExpectResponse(IPutRoleResponse response)
		{
			response.Role.Should().NotBeNull();
			response.Role.Created.Should().BeTrue();
		}
	}

	[Collection(IntegrationContext.Shield)]
	public class PutRoleRunAsApiTests : PutRoleApiTests
	{
		public PutRoleRunAsApiTests(ShieldCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;

		protected override PutRoleRequest Initializer
		{
			get
			{
				var request = base.Initializer;
				request.RequestConfiguration = new RequestConfiguration
				{
					RunAs = ShieldInformation.User.Username
				};
				return request;
			}
		}

		protected override Func<PutRoleDescriptor, IPutRoleRequest> Fluent => f => base.Fluent(f
			.RequestConfiguration(c=>c
				.RunAs(ShieldInformation.User.Username)
			));

		protected override void ExpectResponse(IPutRoleResponse response)
		{
		}
	}

}
