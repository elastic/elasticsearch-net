using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Elastic.Managed.Ephemeral.ClusterAuthentication;

namespace Tests.XPack.Security.User.GetUserAccessToken
{
	[SkipVersion("<5.5.0", "")]
	public class GetUserAccessTokenApiTests : ApiIntegrationTestBase<XPackCluster, IGetUserAccessTokenResponse, IGetUserAccessTokenRequest,
		GetUserAccessTokenDescriptor, GetUserAccessTokenRequest>
	{
		public GetUserAccessTokenApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected virtual string UserPassword => Admin.Password;

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetUserAccessToken(Admin.Username, UserPassword, f),
			fluentAsync: (client, f) => client.GetUserAccessTokenAsync(Admin.Username, UserPassword, f),
			request: (client, r) => client.GetUserAccessToken(r),
			requestAsync: (client, r) => client.GetUserAccessTokenAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => "/_xpack/security/oauth2/token";

		protected override bool SupportsDeserialization => false;

		//callisolated value can sometimes start with a digit which is not allowed for rolenames
		private string Role => $"role-{CallIsolatedValue}";

		protected override object ExpectJson => new
		{
			grant_type = "password",
			scope = "FULL",
			username = Admin.Username,
			password = UserPassword
		};

		protected override GetUserAccessTokenRequest Initializer => new GetUserAccessTokenRequest(Admin.Username, UserPassword)
		{
			Scope = "FULL"
		};

		protected override GetUserAccessTokenDescriptor NewDescriptor() => new GetUserAccessTokenDescriptor(Admin.Username, UserPassword);

		protected override Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> Fluent => d => d
			.Scope("FULL");

		protected override void ExpectResponse(IGetUserAccessTokenResponse response)
		{
			response.AccessToken.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty().And.Be("Bearer");
			response.ExpiresIn.Should().BeGreaterThan(0);
			response.Scope.Should().Be("full");
		}
	}

	public class GetUserAccessTokenBadPasswordApiTests : GetUserAccessTokenApiTests
	{
		public GetUserAccessTokenBadPasswordApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 401;
		protected override string UserPassword => "bad_password";

		protected override void ExpectResponse(IGetUserAccessTokenResponse response)
		{
			response.ServerError.Should().NotBeNull();
		}
	}

}
