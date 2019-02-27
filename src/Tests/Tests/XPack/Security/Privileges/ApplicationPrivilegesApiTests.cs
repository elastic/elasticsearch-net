using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.Privileges
{
	[SkipVersion("<6.4.0", "Only exists in Elasticsearch 6.4.0+")]
	public class ApplicationPrivilegesApiTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string PutPrivilegesStep = nameof(PutPrivilegesStep);
		private const string GetPrivilegesStep = nameof(GetPrivilegesStep);
		private const string PutRoleStep = nameof(PutRoleStep);
		private const string PutUserStep = nameof(PutUserStep);
		private const string HasPrivilegesStep = nameof(HasPrivilegesStep);

		public ApplicationPrivilegesApiTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutPrivilegesStep, u => u.Calls<PutPrivilegesDescriptor, PutPrivilegesRequest, IPutPrivilegesRequest, IPutPrivilegesResponse>(
					v => new PutPrivilegesRequest
					{
						Applications = new AppPrivileges
						{
							{
								$"app-{v}", new Nest.Privileges
								{
									{
										$"p1-{v}", new PrivilegesActions
										{
											Actions = new[] { "data:read/*", "action:login" },
											Metadata = new Dictionary<string, object>
											{
												{ "key1", "val1a" },
												{ "key2", "val2a" }
											}
										}
									}
								}
							}
						}
					},
					(v, d) => d
						.Applications(a => a
							.Application($"app-{v}", pr => pr
								.Privilege($"p1-{v}", ac => ac
									.Actions("data:read/*", "action:login")
									.Metadata(m => m
										.Add("key1", "val1a")
										.Add("key2", "val2a")
									)
								)
							)
						),
					(v, c, f) => c.PutPrivileges(f),
					(v, c, f) => c.PutPrivilegesAsync(f),
					(v, c, r) => c.PutPrivileges(r),
					(v, c, r) => c.PutPrivilegesAsync(r)
				)
			},
			{
				GetPrivilegesStep, u => u.Calls<GetPrivilegesDescriptor, GetPrivilegesRequest, IGetPrivilegesRequest, IGetPrivilegesResponse>(
					v => new GetPrivilegesRequest($"app-{v}", $"p1-{v}"),
					(v, d) => d.Application($"app-{v}").Name($"p1-{v}"),
					(v, c, f) => c.GetPrivileges(f),
					(v, c, f) => c.GetPrivilegesAsync(f),
					(v, c, r) => c.GetPrivileges(r),
					(v, c, r) => c.GetPrivilegesAsync(r)
				)
			},
			{
				PutRoleStep, u => u.Call((v, c) => c.PutRoleAsync($"role-{v}", r=>r
					.Applications(apps=>apps
						.Add<object>(a=>a.Resources("*").Privileges($"p1-{v}").Application($"app-{v}"))
					)
				))
			},
			{
				PutUserStep, u => u.Call((v, c) => c.PutUserAsync($"user-{v}",
					r => r.Roles("admin", $"role-{v}").Password($"pass-{v}")))
			},
			{
				HasPrivilegesStep, u => u.Calls<HasPrivilegesDescriptor, HasPrivilegesRequest, IHasPrivilegesRequest, IHasPrivilegesResponse>(
					v => new HasPrivilegesRequest
					{
						RequestConfiguration = new RequestConfiguration
						{
							BasicAuthenticationCredentials = new BasicAuthenticationCredentials
							{
								Username = $"user-{v}", Password = $"pass-{v}"
							},
						},
						Application = new[]
						{
							new ApplicationPrivilegesCheck
							{
								Name = $"app-{v}",
								Privileges = new[] { "action:login" },
								Resources = new [] {"*"}
							}
						}
					},
					(v, d) => d
						.RequestConfiguration(r=>r.BasicAuthentication($"user-{v}", $"pass-{v}"))
						.Applications(apps => apps
							.Application(a => a
								.Name($"app-{v}")
								.Privileges("action:login")
								.Resources($"p1-{v}")
							)
						)
					,
					(v, c, f) => c.HasPrivileges(f),
					(v, c, f) => c.HasPrivilegesAsync(f),
					(v, c, r) => c.HasPrivileges(r),
					(v, c, r) => c.HasPrivilegesAsync(r)
				)
			}
		}) { }

		[I] public async Task PutPrivilegesResponse() => await Assert<PutPrivilegesResponse>(PutPrivilegesStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Applications.Should().NotBeEmpty();
			var app = $"app-{v}";
			var hasApp = r.Applications.TryGetValue(app, out var privilegesDict);
			hasApp.Should().BeTrue($"expect `{app}` to be returned");
			privilegesDict.Should().NotBeNull($"expect `{app}`'s value not to be null");

			var privilege = $"p1-{v}";
			var hasP1 = privilegesDict.TryGetValue(privilege, out var createdValue);
			hasP1.Should().BeTrue($"expect `{privilege}` to be returned");
			createdValue.Should().NotBeNull($"expect `{privilege}`'s value not to be null");
			createdValue.Created.Should().BeTrue($"expect `{privilege}` to be created in the response");

		});

		[I] public async Task GetPrivilegesResponse() => await Assert<GetPrivilegesResponse>(GetPrivilegesStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Applications.Should().NotBeEmpty();
			var app = $"app-{v}";
			var hasApp = r.Applications.TryGetValue(app, out var privilegesDict);
			hasApp.Should().BeTrue($"expect `{app}` to be returned");
			privilegesDict.Should().NotBeNull($"expect `{app}`'s value not to be null");

			var privilegeName = $"p1-{v}";
			var hasP1 = privilegesDict.TryGetValue(privilegeName, out var privilege);
			hasP1.Should().BeTrue($"expect `{privilegeName}` to be returned");
			privilege.Should().NotBeNull($"expect `{privilegeName}`'s value not to be null");
			privilege.Actions.Should().NotBeEmpty($"expect `{privilegeName}` to return its actions");
			privilege.Metadata.Should().NotBeEmpty($"expect `{privilegeName}` to return its metadata");
		});

		[I] public async Task PutRoleResponse() => await Assert<PutRoleResponse>(PutRoleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.Role.Should().NotBeNull();
			r.Role.Created.Should().BeTrue();
		});

		[I] public async Task PutUserResponse() => await Assert<PutUserResponse>(PutUserStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.User.Should().NotBeNull();
			r.User.Created.Should().BeTrue();
		});

		[I] public async Task HasPrivilegesResponse() => await Assert<HasPrivilegesResponse>(HasPrivilegesStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
		});

	}
}
