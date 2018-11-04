using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.Authenticate
{
	[SkipVersion("<2.3.0", "")]
	public class AuthenticateApiTests
		: ApiIntegrationTestBase<XPackCluster, IAuthenticateResponse, IAuthenticateRequest, AuthenticateDescriptor, AuthenticateRequest>
	{
		public AuthenticateApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_xpack/security/_authenticate";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Authenticate(f),
			(client, f) => client.AuthenticateAsync(f),
			(client, r) => client.Authenticate(r),
			(client, r) => client.AuthenticateAsync(r)
		);

		protected override void ExpectResponse(IAuthenticateResponse response)
		{
			response.Username.Should().Be(ShieldInformation.Admin.Username);
			response.Roles.Should().Contain(ShieldInformation.Admin.Role);
		}
	}

	[SkipVersion("<2.3.0", "")]
	public class AuthenticateRequestConfigurationApiTests : AuthenticateApiTests
	{
		public AuthenticateRequestConfigurationApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<AuthenticateDescriptor, IAuthenticateRequest> Fluent => f => f
			.RequestConfiguration(c => c
				.BasicAuthentication(ShieldInformation.User.Username, ShieldInformation.User.Password)
			);

		protected override AuthenticateRequest Initializer => new AuthenticateRequest
		{
			RequestConfiguration = new RequestConfiguration
			{
				BasicAuthenticationCredentials = new BasicAuthenticationCredentials
				{
					Username = ShieldInformation.User.Username,
					Password = ShieldInformation.User.Password
				}
			}
		};

		protected override void ExpectResponse(IAuthenticateResponse response)
		{
			response.Username.Should().Be(ShieldInformation.User.Username);
			response.Roles.Should().Contain(ShieldInformation.User.Role);
		}
	}
}
