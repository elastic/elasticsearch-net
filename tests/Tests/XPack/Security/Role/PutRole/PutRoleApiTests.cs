// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
 using Elastic.Elasticsearch.Ephemeral;
 using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.XPack.Security.Role.PutRole
{
	[SkipVersion("<2.3.0", "")]
	public class PutRoleApiTests : ApiIntegrationTestBase<XPackCluster, PutRoleResponse, IPutRoleRequest, PutRoleDescriptor, PutRoleRequest>
	{
		public PutRoleApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			cluster = new[] { "all" },
			run_as = new[] { "user" },
			indices = new[]
			{
				new
				{
					names = new[] { "project" },
					privileges = new[] { "all" },
					field_security = new { grant = new[] { "name", "description" } },
					query = new { match_all = new { } }
				}
			},
			metadata = new
			{
				@internal = true
			}
		};

		protected override int ExpectStatusCode => 200;

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
			.Metadata(m => m.Add("internal", true));

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutRoleRequest Initializer => new PutRoleRequest(Role)
		{
			Cluster = new[] { "all" },
			RunAs = new[] { "user" },
			Indices = new List<IIndicesPrivileges>
			{
				new IndicesPrivileges
				{
					FieldSecurity = new FieldSecurity
					{
						Grant = Fields<Project>(p => p.Name).And<Project>(p => p.Description)
					},
					Names = Indices<Project>(),
					Privileges = new[] { "all" },
					Query = new MatchAllQuery()
				}
			},
			Metadata = new Dictionary<string, object>()
			{
				{ "internal", true }
			}
		};

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		protected string Role => $"role-{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_security/role/{Role}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.PutRole(Role, f),
			(client, f) => client.Security.PutRoleAsync(Role, f),
			(client, r) => client.Security.PutRole(r),
			(client, r) => client.Security.PutRoleAsync(r)
		);

		protected override PutRoleDescriptor NewDescriptor() => new PutRoleDescriptor(Role);

		protected override void ExpectResponse(PutRoleResponse response)
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

		protected override Func<PutRoleDescriptor, IPutRoleRequest> Fluent => f => base.Fluent(f
			.RequestConfiguration(c => c
				.RunAs(ClusterAuthentication.User.Username)
			));

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

		protected override void ExpectResponse(PutRoleResponse response) { }
	}

	[SkipVersion("<6.4.0", "Application privileges introduced in 6.4.0")]
	public class PutRoleApplicationsTests : PutRoleApiTests
	{
		public PutRoleApplicationsTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			applications = new[]
			{
				new
				{
					application = "myapp",
					privileges = new[] { "admin", "read" },
					resources = new[] { "*" }
				}
			}
		};

		protected override Func<PutRoleDescriptor, IPutRoleRequest> Fluent => f => f
			.Applications(a => a
				.Add(ap => ap
					.Application("myapp")
					.Privileges("admin", "read")
					.Resources("*")
				)
			);

		protected override PutRoleRequest Initializer => new PutRoleRequest(Role)
		{
			Applications = new List<IApplicationPrivileges>
			{
				new ApplicationPrivileges
				{
					Application = "myapp",
					Privileges = new[] { "admin", "read" },
					Resources = new[] { "*" }
				}
			}
		};
	}
}
