using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Security.ClearCachedRealms
{
	public class ClearCachedRealmsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/security/realm/mpdreamz/_clear_cache")
				.Fluent(c => c.ClearCachedRealms("mpdreamz"))
				.Request(c => c.ClearCachedRealms(new ClearCachedRealmsRequest("mpdreamz")))
				.FluentAsync(c => c.ClearCachedRealmsAsync("mpdreamz"))
				.RequestAsync(c => c.ClearCachedRealmsAsync(new ClearCachedRealmsRequest("mpdreamz")))
				;


			var users = "mpdreamz,gmarz,forloop";
			await POST($"/_xpack/security/realm/mpdreamz/_clear_cache?usernames={EscapeUriString(users)}")
				.Fluent(c => c.ClearCachedRealms("mpdreamz", f=>f.Usernames(users)))
				.Request(c => c.ClearCachedRealms(new ClearCachedRealmsRequest("mpdreamz") { Usernames = new[] { users } }))
				.FluentAsync(c => c.ClearCachedRealmsAsync("mpdreamz", f=>f.Usernames(users)))
				.RequestAsync(c => c.ClearCachedRealmsAsync(new ClearCachedRealmsRequest("mpdreamz") { Usernames = new[] { users } }))
				;
		}
	}
}
