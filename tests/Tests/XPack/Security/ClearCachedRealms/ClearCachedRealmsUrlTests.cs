// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Security.ClearCachedRealms
{
	public class ClearCachedRealmsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_security/realm/mpdreamz/_clear_cache")
					.Fluent(c => c.Security.ClearCachedRealms("mpdreamz"))
					.Request(c => c.Security.ClearCachedRealms(new ClearCachedRealmsRequest("mpdreamz")))
					.FluentAsync(c => c.Security.ClearCachedRealmsAsync("mpdreamz"))
					.RequestAsync(c => c.Security.ClearCachedRealmsAsync(new ClearCachedRealmsRequest("mpdreamz")))
				;


			var users = "mpdreamz,gmarz,forloop";
			await POST($"/_security/realm/mpdreamz/_clear_cache?usernames={EscapeUriString(users)}")
					.Fluent(c => c.Security.ClearCachedRealms("mpdreamz", f => f.Usernames(users)))
					.Request(c => c.Security.ClearCachedRealms(new ClearCachedRealmsRequest("mpdreamz") { Usernames = new[] { users } }))
					.FluentAsync(c => c.Security.ClearCachedRealmsAsync("mpdreamz", f => f.Usernames(users)))
					.RequestAsync(c => c.Security.ClearCachedRealmsAsync(new ClearCachedRealmsRequest("mpdreamz") { Usernames = new[] { users } }))
				;
		}
	}
}
