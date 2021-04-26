/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Configuration;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Elasticsearch.Ephemeral.ClusterAuthentication;

namespace Tests.XPack.Security.User.GetUserAccessToken
{
	[SkipVersion("<5.5.0", "")]
	public class GetUserAccessTokenApiTests
		: ApiIntegrationTestBase<Security, GetUserAccessTokenResponse, IGetUserAccessTokenRequest,
			GetUserAccessTokenDescriptor, GetUserAccessTokenRequest>
	{
		public GetUserAccessTokenApiTests(Security cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			grant_type = "password",
			scope = "FULL",
			username = Admin.Username,
			password = UserPassword
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> Fluent => d => d
			.Scope("FULL");

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override GetUserAccessTokenRequest Initializer => new GetUserAccessTokenRequest(Admin.Username, UserPassword)
		{
			Scope = "FULL"
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => "/_security/oauth2/token";

		protected virtual string UserPassword => Admin.Password;

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.GetUserAccessToken(Admin.Username, UserPassword, f),
			(client, f) => client.Security.GetUserAccessTokenAsync(Admin.Username, UserPassword, f),
			(client, r) => client.Security.GetUserAccessToken(r),
			(client, r) => client.Security.GetUserAccessTokenAsync(r)
		);

		protected override GetUserAccessTokenDescriptor NewDescriptor() => new GetUserAccessTokenDescriptor(Admin.Username, UserPassword);

		protected override void ExpectResponse(GetUserAccessTokenResponse response)
		{
			response.AccessToken.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty().And.Be("Bearer");
			response.ExpiresIn.Should().BeGreaterThan(0);
			response.Scope.Should().Be("full");

			if (TestConfiguration.Instance.InRange(">=7.11.0"))
			{
				response.Authentication.Should().NotBeNull();
				response.Authentication.Username.Should().NotBeNullOrEmpty();
				response.Authentication.Roles.Count.Should().BeGreaterThan(0);
			}
		}
	}

	public class GetUserAccessTokenBadPasswordApiTests : GetUserAccessTokenApiTests
	{
		public GetUserAccessTokenBadPasswordApiTests(Security cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 401;
		protected override string UserPassword => "bad_password";

		protected override void ExpectResponse(GetUserAccessTokenResponse response) => response.ServerError.Should().NotBeNull();
	}
}
