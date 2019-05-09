using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.EndpointTests.TestState;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.ApiKey
{
	[SkipVersion("<6.7.0", "Security Api Keys are modelled against 6.7.0")]
	public class SecurityApiKeyTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string PutRoleStep = nameof(PutRoleStep);
		private const string PutUserStep = nameof(PutUserStep);
		private const string PutPrivilegesStep = nameof(PutPrivilegesStep);
		private const string CreateApiKeyWithRolesStep = nameof(CreateApiKeyWithRolesStep);
		private const string CreateApiKeyWithNoRolesStep = nameof(CreateApiKeyWithNoRolesStep);
		private const string GetApiKeyStep = nameof(GetApiKeyStep);
		private const string InvalidateApiKeyStep = nameof(InvalidateApiKeyStep);

		public SecurityApiKeyTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutRoleStep, u =>
					u.Calls<PutRoleDescriptor, PutRoleRequest, IPutRoleRequest, PutRoleResponse>(
						v => new PutRoleRequest($"role-{v}")
						{
							Cluster = new[] { "all" },
							Indices = new IIndicesPrivileges[]
							{
								new IndicesPrivileges
								{
									Names = "*",
									Privileges = new [] { "all" }
								}
							},
							Applications = new IApplicationPrivileges[]
							{
								new ApplicationPrivileges
								{
									Application = $"app-{v}",
									Privileges = new [] { "*" },
									Resources = new [] { "*" }
								}
							}
						},
						(v, d) => d
							.Cluster("all")
							.Indices(i => i.Add<object>(p => p.Names("*").Privileges("all")))
							.Applications(i => i.Add(p => p.Application($"app-{v}").Privileges("*").Resources("*")))
						,
						(v, c, f) => c.PutRole($"role-{v}", f),
						(v, c, f) => c.PutRoleAsync($"role-{v}", f),
						(v, c, r) => c.PutRole(r),
						(v, c, r) => c.PutRoleAsync(r)
					)
			},
			{
				PutUserStep, u =>
					u.Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, PutUserResponse>(
						v => new PutUserRequest($"user-{v}")
						{
							Password = "password",
							Roles = new[] { $"role-{v}", "superuser" },
							FullName = "API key user"
						},
						(v, d) => d
							.Password("password")
							.Roles($"role-{v}", "superuser")
							.FullName("API key user")
						,
						(v, c, f) => c.PutUser($"user-{v}", f),
						(v, c, f) => c.PutUserAsync($"user-{v}", f),
						(v, c, r) => c.PutUser(r),
						(v, c, r) => c.PutUserAsync(r)
					)
			},
			{
				PutPrivilegesStep, u =>
					u.Calls<PutPrivilegesDescriptor, PutPrivilegesRequest, IPutPrivilegesRequest, PutPrivilegesResponse>(
						v => new PutPrivilegesRequest
						{
							Applications = new AppPrivileges
							{
								{
									$"app-{v}", new Nest.Privileges
									{
										{
											$"read", new PrivilegesActions
											{
												Actions = new[] { "data:read/*" }
											}
										},
										{
											$"write", new PrivilegesActions
											{
												Actions = new[] { "data:write/*" }
											}
										}
									}
								}
							}
						},
						(v, d) => d
							.Applications(a => a
								.Application($"app-{v}", pr => pr
									.Privilege($"read", ac => ac
										.Actions("data:read/*")
									)
									.Privilege($"write", ac => ac
										.Actions("data:write/*")
									)
								)
							)
						,
						(v, c, f) => c.PutPrivileges(f),
						(v, c, f) => c.PutPrivilegesAsync(f),
						(v, c, r) => c.PutPrivileges(r),
						(v, c, r) => c.PutPrivilegesAsync(r)
					)
			},
			{
				CreateApiKeyWithRolesStep, u =>
					u.Calls<CreateApiKeyDescriptor, CreateApiKeyRequest, ICreateApiKeyRequest, CreateApiKeyResponse>(
						v => new CreateApiKeyRequest
						{
							Name = v,
							Expiration = "1d",
							Roles = new ApiKeyRoles
							{
								{
									"role-a", new ApiKeyRole
												{
													Cluster = new[] { "all" },
													Index = new []
													{
														new ApiKeyPrivileges
														{
															Names = new [] { "index-a*" },
															Privileges = new[] { "read" }
														}
													}
												}
								},
								{
									"role-b", new ApiKeyRole
												{
													Cluster = new[] { "all" },
													Index = new []
													{
														new ApiKeyPrivileges
														{
															Names = new [] { "index-b*" },
															Privileges = new[] { "read" }
														}
													}
												}
								}
							},
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials
								{
									Username = $"user-{v}",
									Password = "password"
								}
							}
						},
						(v, d) => d
							.Name(v)
							.Expiration("1d")
							.Roles(r => r.Role("role-a", o => o.Cluster("all").Indices(i => i.Index(k => k.Names("index-a").Privileges("read"))))
								         .Role("role-b", o => o.Cluster("all").Indices(i => i.Index(k => k.Names("index-b").Privileges("read")))))
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.CreateApiKey(f),
						(v, c, f) => c.CreateApiKeyAsync(f),
						(v, c, r) => c.CreateApiKey(r),
						(v, c, r) => c.CreateApiKeyAsync(r)
					)
			},
			{
				CreateApiKeyWithNoRolesStep, u =>
					u.Calls<CreateApiKeyDescriptor, CreateApiKeyRequest, ICreateApiKeyRequest, CreateApiKeyResponse>(
						v => new CreateApiKeyRequest
						{
							Name = v,
							Expiration = "1d",
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials
								{
									Username = $"user-{v}",
									Password = "password"
								}
							}
						},
						(v, d) => d
							.Name(v)
							.Expiration("1d")
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.CreateApiKey(f),
						(v, c, f) => c.CreateApiKeyAsync(f),
						(v, c, r) => c.CreateApiKey(r),
						(v, c, r) => c.CreateApiKeyAsync(r)
					)
			},
			{
				GetApiKeyStep, u =>
					u.Calls<GetApiKeyDescriptor, GetApiKeyRequest, IGetApiKeyRequest, GetApiKeyResponse>(
						v => new GetApiKeyRequest
						{
							Name = v,
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials
								{
									Username = $"user-{v}",
									Password = "password"
								}
							}
						},
						(v, d) => d
							.Name(v)
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.GetApiKey(f),
						(v, c, f) => c.GetApiKeyAsync(f),
						(v, c, r) => c.GetApiKey(r),
						(v, c, r) => c.GetApiKeyAsync(r)
					)
			},
			{
				InvalidateApiKeyStep, u =>
					u.Calls<InvalidateApiKeyDescriptor, InvalidateApiKeyRequest, IInvalidateApiKeyRequest, InvalidateApiKeyResponse>(
						v => new InvalidateApiKeyRequest
						{
							Name = v,
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials
								{
									Username = $"user-{v}",
									Password = "password"
								}
							}
						},
						(v, d) => d
							.Name(v)
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.InvalidateApiKey(f),
						(v, c, f) => c.InvalidateApiKeyAsync(f),
						(v, c, r) => c.InvalidateApiKey(r),
						(v, c, r) => c.InvalidateApiKeyAsync(r)
					)
			}
		}) { }

		[I] public async Task SecurityCreateApiKeyResponse() => await Assert<CreateApiKeyResponse>(CreateApiKeyWithRolesStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.Id.Should().NotBeNullOrEmpty();
			r.Name.Should().NotBeNullOrEmpty();
			r.Expiration.Should().NotBeNull();
			r.ApiKey.Should().NotBeNullOrEmpty();
		});

		[I] public async Task SecurityGetApiKeyResponse() => await Assert<GetApiKeyResponse>(GetApiKeyStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiKeys.Should().NotBeNullOrEmpty();

			foreach (var apiKey in r.ApiKeys)
			{
				apiKey.Id.Should().NotBeNullOrEmpty();
				apiKey.Name.Should().NotBeNullOrEmpty();
				apiKey.Creation.Should().BeBefore(DateTimeOffset.UtcNow);
				apiKey.Expiration.Should().BeAfter(DateTimeOffset.UtcNow);
				apiKey.Invalidated.Should().Be(false);
				apiKey.Username.Should().NotBeNullOrEmpty();
				apiKey.Realm.Should().NotBeNullOrEmpty();
			}
		});

		[I] public async Task SecurityInvalidateApiKeyResponse() => await Assert<InvalidateApiKeyResponse>(InvalidateApiKeyStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.ErrorCount.Should().Be(0);
			r.PreviouslyInvalidatedApiKeys.Should().BeEmpty();
			r.InvalidatedApiKeys.Should().HaveCount(2);
		});
	}
}
