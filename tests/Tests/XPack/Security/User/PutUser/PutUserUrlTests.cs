// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Security.User.PutUser
{
	public class PutUserUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.PUT("/_security/user/mpdreamz")
			.Fluent(c => c.Security.PutUser("mpdreamz", p => p))
			.Request(c => c.Security.PutUser(new PutUserRequest("mpdreamz")))
			.FluentAsync(c => c.Security.PutUserAsync("mpdreamz", p => p))
			.RequestAsync(c => c.Security.PutUserAsync(new PutUserRequest("mpdreamz")));
	}
}
