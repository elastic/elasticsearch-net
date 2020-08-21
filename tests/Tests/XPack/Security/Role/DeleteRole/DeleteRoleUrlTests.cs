// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.Role.DeleteRole
{
	public class DeleteRoleUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_security/role/mpdreamz")
			.Fluent(c => c.Security.DeleteRole("mpdreamz"))
			.Request(c => c.Security.DeleteRole(new DeleteRoleRequest("mpdreamz")))
			.FluentAsync(c => c.Security.DeleteRoleAsync("mpdreamz"))
			.RequestAsync(c => c.Security.DeleteRoleAsync(new DeleteRoleRequest("mpdreamz")));
	}
}
