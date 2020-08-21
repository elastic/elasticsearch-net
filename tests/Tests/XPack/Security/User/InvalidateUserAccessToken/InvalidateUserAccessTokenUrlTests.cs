// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.User.InvalidateUserAccessToken
{
	public class InvalidateUserAccessTokenUserUrlTests : UrlTestsBase
	{
		[U]
		public override async Task Urls()
		{
			var token = "some_token";
			await DELETE("/_security/oauth2/token")
				.Fluent(c => c.Security.InvalidateUserAccessToken(token))
				.Request(c => c.Security.InvalidateUserAccessToken(new InvalidateUserAccessTokenRequest(token)))
				.FluentAsync(c => c.Security.InvalidateUserAccessTokenAsync(token))
				.RequestAsync(c => c.Security.InvalidateUserAccessTokenAsync(new InvalidateUserAccessTokenRequest(token)));
		}
	}
}
