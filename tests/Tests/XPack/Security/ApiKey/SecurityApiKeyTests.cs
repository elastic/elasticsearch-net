// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.ApiKey
{
	[SkipVersion("<6.7.0", "Security Api Keys are modelled against 6.7.0")]
	public class SecurityApiKeyTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string PutUserStep = nameof(PutUserStep);
		private const string CreateApiKeyWithRolesStep = nameof(CreateApiKeyWithRolesStep);
		private const string CreateApiKeyWithNoRolesStep = nameof(CreateApiKeyWithNoRolesStep);
		private const string GetApiKeyStep = nameof(GetApiKeyStep);
		private const string GetAllApiKeysStep = nameof(GetAllApiKeysStep);
		private const string InvalidateApiKeyStep = nameof(InvalidateApiKeyStep);
		private const string GetAnotherApiKeyStep = nameof(GetAnotherApiKeyStep);
		private const string ClearApiKeyCacheStep = nameof(ClearApiKeyCacheStep);
		private const string ClearAllApiKeyCacheStep = nameof(ClearAllApiKeyCacheStep);

		public SecurityApiKeyTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutUserStep, u =>
					u.Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, PutUserResponse>(
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
						(v, c, f) => c.Security.PutUser($"user-{v}", f),
						(v, c, f) => c.Security.PutUserAsync($"user-{v}", f),
						(v, c, r) => c.Security.PutUser(r),
						(v, c, r) => c.Security.PutUserAsync(r)
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
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}", "password")
							}
						},
						(v, d) => d
							.Name(v)
							.Expiration("1d")
							.Roles(r => r.Role("role-a", o => o.Cluster("all").Indices(i => i.Index(k => k.Names("index-a").Privileges("read"))))
								         .Role("role-b", o => o.Cluster("all").Indices(i => i.Index(k => k.Names("index-b").Privileges("read")))))
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.Security.CreateApiKey(f),
						(v, c, f) => c.Security.CreateApiKeyAsync(f),
						(v, c, r) => c.Security.CreateApiKey(r),
						(v, c, r) => c.Security.CreateApiKeyAsync(r)
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
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}", "password")
							}
						},
						(v, d) => d
							.Name(v)
							.Expiration("1d")
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.Security.CreateApiKey(f),
						(v, c, f) => c.Security.CreateApiKeyAsync(f),
						(v, c, r) => c.Security.CreateApiKey(r),
						(v, c, r) => c.Security.CreateApiKeyAsync(r)
					)
			},
			{
				// This was fixed in 7.5.0
				GetAllApiKeysStep, ">=7.5.0", u =>
					u.Calls<GetApiKeyDescriptor, GetApiKeyRequest, IGetApiKeyRequest, GetApiKeyResponse>(
						v => new GetApiKeyRequest
						{
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}", "password")
							}
						},
						(v, d) => d
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.Security.GetApiKey(f),
						(v, c, f) => c.Security.GetApiKeyAsync(f),
						(v, c, r) => c.Security.GetApiKey(r),
						(v, c, r) => c.Security.GetApiKeyAsync(r)
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
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}", "password")
							}
						},
						(v, d) => d
							.Name(v)
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.Security.GetApiKey(f),
						(v, c, f) => c.Security.GetApiKeyAsync(f),
						(v, c, r) => c.Security.GetApiKey(r),
						(v, c, r) => c.Security.GetApiKeyAsync(r)
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
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}","password")
							}
						},
						(v, d) => d
							.Name(v)
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.Security.InvalidateApiKey(f),
						(v, c, f) => c.Security.InvalidateApiKeyAsync(f),
						(v, c, r) => c.Security.InvalidateApiKey(r),
						(v, c, r) => c.Security.InvalidateApiKeyAsync(r)
					)
			},
			{
				GetAnotherApiKeyStep, u =>
					u.Calls<GetApiKeyDescriptor, GetApiKeyRequest, IGetApiKeyRequest, GetApiKeyResponse>(
						v => new GetApiKeyRequest
						{
							Name = v,
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}", "password")
							}
						},
						(v, d) => d
							.Name(v)
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}", "password"))
						,
						(v, c, f) => c.Security.GetApiKey(f),
						(v, c, f) => c.Security.GetApiKeyAsync(f),
						(v, c, r) => c.Security.GetApiKey(r),
						(v, c, r) => c.Security.GetApiKeyAsync(r),
						(r, values) => values.ExtendedValue("apiKey", r.ApiKeys.FirstOrDefault()?.Id ?? string.Empty)
					)
			},
			{
				// API introduced in 7.10.0
				ClearApiKeyCacheStep, ">=7.10.0", u =>
					u.Calls<ClearApiKeyCacheDescriptor, ClearApiKeyCacheRequest, IClearApiKeyCacheRequest, ClearApiKeyCacheResponse>(
						v => new ClearApiKeyCacheRequest(u.Usage.CallUniqueValues.ExtendedValue<string>("apiKey") ?? string.Empty),
						(v, d) => d,
						(v, c, f) => c.Security.ClearApiKeyCache(f => f.Ids(u.Usage.CallUniqueValues.ExtendedValue<string>("apiKey"))),
						(v, c, f) => c.Security.ClearApiKeyCacheAsync(f => f.Ids(u.Usage.CallUniqueValues.ExtendedValue<string>("apiKey"))),
						(v, c, r) => c.Security.ClearApiKeyCache(r),
						(v, c, r) => c.Security.ClearApiKeyCacheAsync(r)
					)
			},
			{
				// API introduced in 7.10.0
				ClearAllApiKeyCacheStep, ">=7.10.0", u =>
					u.Calls<ClearApiKeyCacheDescriptor, ClearApiKeyCacheRequest, IClearApiKeyCacheRequest, ClearApiKeyCacheResponse>(
						v => new ClearApiKeyCacheRequest(),
						(v, d) => d,
						(v, c, f) => c.Security.ClearApiKeyCache(),
						(v, c, f) => c.Security.ClearApiKeyCacheAsync(),
						(v, c, r) => c.Security.ClearApiKeyCache(r),
						(v, c, r) => c.Security.ClearApiKeyCacheAsync(r)
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

		[I] public async Task SecurityGetAllApiKeysResponse() => await Assert<GetApiKeyResponse>(GetAllApiKeysStep, r =>
		{
			r.IsValid.Should().BeTrue();
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

		[I] public async Task SecurityClearApiKeyCacheResponse() => await Assert<ClearApiKeyCacheResponse>(ClearApiKeyCacheStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.NodeStatistics.Successful.Should().BeGreaterOrEqualTo(1);
			r.ClusterName.Should().NotBeNullOrEmpty();
			r.Nodes.Count.Should().BeGreaterOrEqualTo(1);
		});

		[I] public async Task SecurityClearAllApiKeyCacheResponse() => await Assert<ClearApiKeyCacheResponse>(ClearAllApiKeyCacheStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.NodeStatistics.Successful.Should().BeGreaterOrEqualTo(1);
			r.ClusterName.Should().NotBeNullOrEmpty();
			r.Nodes.Count.Should().BeGreaterOrEqualTo(1);
		});
	}
}
