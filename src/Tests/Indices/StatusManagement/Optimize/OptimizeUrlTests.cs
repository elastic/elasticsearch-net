using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.Optimize
{
	public class OptimizeUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_optimize")
				.Fluent(c => c.Optimize(All))
				.Request(c => c.Optimize(new OptimizeRequest()))
				.FluentAsync(c => c.OptimizeAsync(All))
				.RequestAsync(c => c.OptimizeAsync(new OptimizeRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_optimize")
				.Fluent(c => c.Optimize(index))
				.Request(c => c.Optimize(new OptimizeRequest(index)))
				.FluentAsync(c => c.OptimizeAsync(index))
				.RequestAsync(c => c.OptimizeAsync(new OptimizeRequest(index)))
				;
		}
	}
}
