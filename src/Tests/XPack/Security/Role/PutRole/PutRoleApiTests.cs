using System;
using System.Collections.Generic;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.XPack.Security.Role.PutRole
{
	[SkipVersion("<2.3.0", "")]
	public class PutRoleApiTests : ApiIntegrationTestBase<XPackCluster, IPutRoleResponse, IPutRoleRequest, PutRoleDescriptor, PutRoleRequest>
	{
		public PutRoleApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutRole(this.Role, f),
			fluentAsync: (client, f) => client.PutRoleAsync(this.Role, f),
			request: (client, r) => client.PutRole(r),
			requestAsync: (client, r) => client.PutRoleAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"/_xpack/security/role/{this.Role}";

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
					field_security = new { grant = new [] { "name", "description" } },
					query = new { match_all = new {} }
				}
			},
			metadata = new
			{
				@internal = true
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
					FieldSecurity = new FieldSecurity
					{
						Grant = Fields<Project>(p=>p.Name).And<Project>(p=>p.Description)
					},
					Names = Indices<Project>(),
					Privileges = new [] { "all" },
					Query = new MatchAllQuery()
				}
			},
			Metadata = new Dictionary<string, object>()
			{
				{ "internal", true }
			}
		};

		protected override PutRoleDescriptor NewDescriptor() => new PutRoleDescriptor(this.Role);

		protected override Func<PutRoleDescriptor, IPutRoleRequest> Fluent => d => d
			.RunAs("user")
			.Cluster("all")
			.Indices(i => i
				.Add<Project>(ii => ii
					.FieldSecurity(fs => fs
						.Grant(f => f
							.Field(p => p.Name)
							.Field(p => p.Description)
						)
					)
					.Names(Indices<Project>())
					.Privileges("all")
					.Query(q => q.MatchAll())
				)
			)
			.Metadata( m => m.Add("internal", true));

		protected override void ExpectResponse(IPutRoleResponse response)
		{
			response.Role.Should().NotBeNull();
			response.Role.Created.Should().BeTrue();
		}
	}

	public class PutRoleRunAsApiTests : PutRoleApiTests
	{
		public PutRoleRunAsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;

		protected override PutRoleRequest Initializer
		{
			get
			{
				var request = base.Initializer;
				request.RequestConfiguration = new RequestConfiguration
				{
					RunAs = ClusterAuthentication.User.Username
				};
				return request;
			}
		}

		protected override Func<PutRoleDescriptor, IPutRoleRequest> Fluent => f => base.Fluent(f
			.RequestConfiguration(c => c
				.RunAs(ClusterAuthentication.User.Username)
			));

		protected override void ExpectResponse(IPutRoleResponse response)
		{
		}
	}

}
