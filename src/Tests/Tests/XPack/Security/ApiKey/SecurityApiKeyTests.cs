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

namespace Tests.XPack.ApiKey
{
	[SkipVersion("<6.7.0", "Security Api Keys are modelled against 6.7.0")]
	public class SecurityApiKeyTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string PutUserStep = nameof(PutUserStep);
		private const string CreateApiKeyWithRolesStep = nameof(CreateApiKeyWithRolesStep);
		private const string CreateApiKeyWithNoRolesStep = nameof(CreateApiKeyWithNoRolesStep);
		private const string GetApiKeyStep = nameof(GetApiKeyStep);
		private const string InvalidateApiKeyStep = nameof(InvalidateApiKeyStep);

		public SecurityApiKeyTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutUserStep, u =>
					u.Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, IPutUserResponse>(
						v => new PutUserRequest($"user-{v}")
						{
							Password = "password",
							Roles = new[] { "superuser" },
							FullName = "API key superuser"
						},
						(v, d) => d
							.Password("password")
							.Roles("superuser")
							.FullName("API key superuser")
						,
						(v, c, f) => c.PutUser($"user-{v}", f),
						(v, c, f) => c.PutUserAsync($"user-{v}", f),
						(v, c, r) => c.PutUser(r),
						(v, c, r) => c.PutUserAsync(r)
					)
			},
			{
				CreateApiKeyWithRolesStep, u =>
					u.Calls<CreateApiKeyDescriptor, CreateApiKeyRequest, ICreateApiKeyRequest, ICreateApiKeyResponse>(
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
					u.Calls<CreateApiKeyDescriptor, CreateApiKeyRequest, ICreateApiKeyRequest, ICreateApiKeyResponse>(
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
					u.Calls<GetApiKeyDescriptor, GetApiKeyRequest, IGetApiKeyRequest, IGetApiKeyResponse>(
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
					u.Calls<InvalidateApiKeyDescriptor, InvalidateApiKeyRequest, IInvalidateApiKeyRequest, IInvalidateApiKeyResponse>(
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
