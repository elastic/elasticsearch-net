using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.ClearCache
{
	public class ClearCacheUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_all/_cache/clear")
					.Fluent(c => c.ClearCache(All))
					.Request(c => c.ClearCache(new ClearCacheRequest(All)))
					.FluentAsync(c => c.ClearCacheAsync(All))
					.RequestAsync(c => c.ClearCacheAsync(new ClearCacheRequest(All)))
				;
			await POST($"/_cache/clear")
					.Request(c => c.ClearCache(new ClearCacheRequest()))
					.RequestAsync(c => c.ClearCacheAsync(new ClearCacheRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_cache/clear")
					.Fluent(c => c.ClearCache(index))
					.Request(c => c.ClearCache(new ClearCacheRequest(index)))
					.FluentAsync(c => c.ClearCacheAsync(index))
					.RequestAsync(c => c.ClearCacheAsync(new ClearCacheRequest(index)))
				;
		}
	}
}
