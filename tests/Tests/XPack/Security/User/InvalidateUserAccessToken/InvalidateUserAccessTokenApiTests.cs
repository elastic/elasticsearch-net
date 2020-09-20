// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Elasticsearch.Ephemeral.ClusterAuthentication;

namespace Tests.XPack.Security.User.InvalidateUserAccessToken
{
	[SkipVersion("<5.5.0", "")]
	public class InvalidateUserAccessTokenApiTests
		: ApiIntegrationTestBase<Security, InvalidateUserAccessTokenResponse, IInvalidateUserAccessTokenRequest,
			InvalidateUserAccessTokenDescriptor, InvalidateUserAccessTokenRequest>
	{
		protected const string AccessTokenValueKey = "accesstoken";

		public InvalidateUserAccessTokenApiTests(Security cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected virtual string CurrentAccessToken => RanIntegrationSetup ? ExtendedValue<string>(AccessTokenValueKey) : "foo";

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			token = CurrentAccessToken
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override InvalidateUserAccessTokenRequest Initializer => new InvalidateUserAccessTokenRequest(CurrentAccessToken);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => "/_security/oauth2/token";

		protected override void OnBeforeCall(IElasticClient client)
		{
			var r = client.Security.GetUserAccessToken(Admin.Username, Admin.Password);
			r.ShouldBeValid();
			ExtendedValue(AccessTokenValueKey, r.AccessToken);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.InvalidateUserAccessToken(CurrentAccessToken, f),
			(client, f) => client.Security.InvalidateUserAccessTokenAsync(CurrentAccessToken, f),
			(client, r) => client.Security.InvalidateUserAccessToken(r),
			(client, r) => client.Security.InvalidateUserAccessTokenAsync(r)
		);

		protected override InvalidateUserAccessTokenDescriptor NewDescriptor() => new InvalidateUserAccessTokenDescriptor(CurrentAccessToken);

		protected override void ExpectResponse(InvalidateUserAccessTokenResponse response) => response.InvalidatedTokens.Should().BeGreaterThan(0);
	}

	public class InvalidateUserAccessTokenBadPasswordApiTests : InvalidateUserAccessTokenApiTests
	{
		public InvalidateUserAccessTokenBadPasswordApiTests(Security cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string CurrentAccessToken => "bad_password";

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => TestClient.Configuration.InRange("<7.8.0") ? 401 : 404;

		protected override void ExpectResponse(InvalidateUserAccessTokenResponse response)
		{
			if (TestClient.Configuration.InRange("<7.8.0"))
				response.ServerError.Should().NotBeNull();
			else
			{
				response.InvalidatedTokens.Should().BeGreaterOrEqualTo(0);
				response.PreviouslyInvalidatedTokens.Should().BeGreaterOrEqualTo(0);
				response.ErrorCount.Should().BeGreaterOrEqualTo(0);
			}
		}
	}
}
