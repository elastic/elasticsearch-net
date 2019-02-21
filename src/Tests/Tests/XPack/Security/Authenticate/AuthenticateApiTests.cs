using System;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using static Elastic.Managed.Ephemeral.ClusterAuthentication;

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

		protected override string UrlPath => $"/_security/_authenticate";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Authenticate(f),
			(client, f) => client.AuthenticateAsync(f),
			(client, r) => client.Authenticate(r),
			(client, r) => client.AuthenticateAsync(r)
		);

		protected override void ExpectResponse(IAuthenticateResponse response)
		{
			response.Username.Should().Be(Admin.Username);
			response.Roles.Should().Contain(Admin.Role);
		}
	}

	[SkipVersion("<2.3.0", "")]
	public class AuthenticateRequestConfigurationApiTests : AuthenticateApiTests
	{
		public AuthenticateRequestConfigurationApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<AuthenticateDescriptor, IAuthenticateRequest> Fluent => f => f
			.RequestConfiguration(c => c
				.BasicAuthentication(ClusterAuthentication.User.Username, ClusterAuthentication.User.Password)
			);

		protected override AuthenticateRequest Initializer => new AuthenticateRequest
		{
			RequestConfiguration = new RequestConfiguration
			{
				BasicAuthenticationCredentials = new BasicAuthenticationCredentials
				{
					Username = ClusterAuthentication.User.Username,
					Password = ClusterAuthentication.User.Password
				}
			}
		};

		protected override void ExpectResponse(IAuthenticateResponse response)
		{
			response.Username.Should().Be(ClusterAuthentication.User.Username);
			response.Roles.Should().Contain(ClusterAuthentication.User.Role);
		}
	}
}
