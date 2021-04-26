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
