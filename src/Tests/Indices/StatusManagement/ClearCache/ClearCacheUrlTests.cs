using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.StatusManagement.ClearCache
{
	public class ClearCacheUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_cache/clear")
				.Fluent(c => c.ClearCache(Nest_5_2_0.Indices.All))
				.Request(c => c.ClearCache(new ClearCacheRequest()))
				.FluentAsync(c => c.ClearCacheAsync(Nest_5_2_0.Indices.All))
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
