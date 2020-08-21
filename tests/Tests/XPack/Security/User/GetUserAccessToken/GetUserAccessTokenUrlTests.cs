// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Ephemeral;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.User.GetUserAccessToken
{
	public class GetUserAccessTokenUserUrlTests : UrlTestsBase
	{
		[U]
		public override async Task Urls()
		{
			var u = ClusterAuthentication.Admin.Username;
			var p = ClusterAuthentication.Admin.Password;
			await POST("/_security/oauth2/token")
				.Fluent(c => c.Security.GetUserAccessToken(u, p))
				.Request(c => c.Security.GetUserAccessToken(new GetUserAccessTokenRequest(u, p)))
				.FluentAsync(c => c.Security.GetUserAccessTokenAsync(u, p))
				.RequestAsync(c => c.Security.GetUserAccessTokenAsync(new GetUserAccessTokenRequest(u, p)));
		}
	}
}
