// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.User.DeleteUser
{
	public class DeleteUserUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_security/user/mpdreamz")
			.Fluent(c => c.Security.DeleteUser("mpdreamz"))
			.Request(c => c.Security.DeleteUser(new DeleteUserRequest("mpdreamz")))
			.FluentAsync(c => c.Security.DeleteUserAsync("mpdreamz"))
			.RequestAsync(c => c.Security.DeleteUserAsync(new DeleteUserRequest("mpdreamz")));
	}
}
