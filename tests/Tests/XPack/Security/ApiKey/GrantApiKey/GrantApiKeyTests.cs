// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.ApiKey.GrantApiKey
{
	[SkipVersion("<7.10.0", "APIs introduced in 7.10.0")]
	public class GrantApiKeyTests : CoordinatedIntegrationTestBase<XPackCluster>
	{
		private const string PutAdminUserStep = nameof(PutAdminUserStep);
		private const string PutTargetUserStep = nameof(PutTargetUserStep);
		private const string GrantApiKeyStep = nameof(GrantApiKeyStep);
		private const string GrantApiKeyWithExpirationStep = nameof(GrantApiKeyWithExpirationStep);
		private const string GenerateAccessTokenStep = nameof(GenerateAccessTokenStep);
		private const string GrantApiKeyUsingAccessTokenStep = nameof(GrantApiKeyUsingAccessTokenStep);
		
		public GrantApiKeyTests(XPackCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutAdminUserStep, u =>
					u.Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, PutUserResponse>(
						v => new PutUserRequest($"user-{v}-admin")
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
						(v, c, f) => c.Security.PutUser($"user-{v}-admin", f),
						(v, c, f) => c.Security.PutUserAsync($"user-{v}-admin", f),
						(v, c, r) => c.Security.PutUser(r),
						(v, c, r) => c.Security.PutUserAsync(r)
					)
			},
			{
				PutTargetUserStep, u =>
					u.Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, PutUserResponse>(
						v => new PutUserRequest($"user-{v}-target")
						{
							Password = "password",
							Roles = new[] { "basic" },
							FullName = "API key target"
						},
						(v, d) => d
							.Password("password")
							.Roles("basic")
							.FullName("API key target")
						,
						(v, c, f) => c.Security.PutUser($"user-{v}-target", f),
						(v, c, f) => c.Security.PutUserAsync($"user-{v}-target", f),
						(v, c, r) => c.Security.PutUser(r),
						(v, c, r) => c.Security.PutUserAsync(r)
					)
			},
			{
				GrantApiKeyStep, u =>
					u.Calls<GrantApiKeyDescriptor, GrantApiKeyRequest, IGrantApiKeyRequest, GrantApiKeyResponse>(
						v => new GrantApiKeyRequest
						{
							GrantType = GrantType.Password,
							Username = $"user-{v}-target",
							Password = "password",
							ApiKey = new Nest.ApiKey
							{
								Name = $"api-key-{v}"
							},
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}-admin", "password")
							}
						},
						(v, d) => d
							.Username($"user-{v}-target")
							.Password("password")
							.ApiKey(k => k.Name($"api-key-{v}"))
							.GrantType(GrantType.Password)
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}-admin", "password")),
						(v, c, f) => c.Security.GrantApiKey(f),
						(v, c, f) => c.Security.GrantApiKeyAsync(f),
						(v, c, r) => c.Security.GrantApiKey(r),
						(v, c, r) => c.Security.GrantApiKeyAsync(r)
					)
			},
			{
				GrantApiKeyWithExpirationStep, u =>
					u.Calls<GrantApiKeyDescriptor, GrantApiKeyRequest, IGrantApiKeyRequest, GrantApiKeyResponse>(
						v => new GrantApiKeyRequest
						{
							GrantType = GrantType.Password,
							Username = $"user-{v}-target",
							Password = "password",
							ApiKey = new Nest.ApiKey
							{
								Name = $"api-key-{v}",
								Expiration = new Time(TimeSpan.FromMinutes(1))
							},
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}-admin", "password")
							}
						},
						(v, d) => d
							.Username($"user-{v}-target")
							.Password("password")
							.ApiKey(k => k
								.Name($"api-key-{v}")
								.Expiration(new Time(TimeSpan.FromMinutes(1))))
							.GrantType(GrantType.Password)
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}-admin", "password")),
						(v, c, f) => c.Security.GrantApiKey(f),
						(v, c, f) => c.Security.GrantApiKeyAsync(f),
						(v, c, r) => c.Security.GrantApiKey(r),
						(v, c, r) => c.Security.GrantApiKeyAsync(r)
					)
			},
			{
				GenerateAccessTokenStep, u =>
					u.Calls<GetUserAccessTokenDescriptor, GetUserAccessTokenRequest, IGetUserAccessTokenRequest, GetUserAccessTokenResponse>(
						v => new GetUserAccessTokenRequest($"user-{v}-target","password"),
						(v, d) => d,
						(v, c, f) => c.Security.GetUserAccessToken($"user-{v}-target", "password", f),
						(v, c, f) => c.Security.GetUserAccessTokenAsync($"user-{v}-target", "password", f),
						(_, c, r) => c.Security.GetUserAccessToken(r),
						(_, c, r) => c.Security.GetUserAccessTokenAsync(r),
						(r, values) => values.ExtendedValue("accessToken", r.AccessToken)
					)
			},
			{
				GrantApiKeyUsingAccessTokenStep, u =>
					u.Calls<GrantApiKeyDescriptor, GrantApiKeyRequest, IGrantApiKeyRequest, GrantApiKeyResponse>(v => new GrantApiKeyRequest
						{
							GrantType = GrantType.AccessToken,
							AccessToken = u.Usage.CallUniqueValues.ExtendedValue<string>("accessToken") ?? string.Empty,
							ApiKey = new Nest.ApiKey
							{
								Name = $"api-key-{v}"
							},
							RequestConfiguration = new RequestConfiguration
							{
								BasicAuthenticationCredentials = new BasicAuthenticationCredentials($"user-{v}-admin", "password")
							}
						},
						(v, d) => d
							.GrantType(GrantType.AccessToken)
							.AccessToken(u.Usage.CallUniqueValues.ExtendedValue<string>("accessToken") ?? string.Empty)
							.ApiKey(k => k.Name($"api-key-{v}"))
							.RequestConfiguration(r => r.BasicAuthentication($"user-{v}-admin", "password")),
						(_, c, f) => c.Security.GrantApiKey(f),
						(_, c, f) => c.Security.GrantApiKeyAsync(f),
						(_, c, r) => c.Security.GrantApiKey(r),
						(_, c, r) => c.Security.GrantApiKeyAsync(r)
					)
			}
		}) { }

		[I] public async Task GrantApiKeyResponse() => await Assert<GrantApiKeyResponse>(GrantApiKeyStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.Id.Should().NotBeNullOrEmpty();
			r.Name.Should().NotBeNullOrEmpty();
			r.ApiKey.Should().NotBeNullOrEmpty();
		});

		[I] public async Task GrantApiKeyWithExpirationResponse() => await Assert<GrantApiKeyResponse>(GrantApiKeyWithExpirationStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.Id.Should().NotBeNullOrEmpty();
			r.Name.Should().NotBeNullOrEmpty();
			r.Expiration.Should().NotBeNull();
			r.ApiKey.Should().NotBeNullOrEmpty();
		});

		[I] public async Task GrantApiKeyWithAccessTokenResponse() => await Assert<GrantApiKeyResponse>(GrantApiKeyUsingAccessTokenStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.Id.Should().NotBeNullOrEmpty();
			r.Name.Should().NotBeNullOrEmpty();
			r.ApiKey.Should().NotBeNullOrEmpty();
		});
	}
}
