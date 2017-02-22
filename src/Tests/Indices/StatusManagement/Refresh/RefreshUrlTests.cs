using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using static Tests.Framework.UrlTester;
using static Nest_5_2_0.Indices;

namespace Tests.Indices.StatusManagement.Refresh
{
	public class RefreshUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_refresh")
				.Fluent(c => c.Refresh(All))
				.Request(c => c.Refresh(new RefreshRequest()))
				.FluentAsync(c => c.RefreshAsync(All))
				.RequestAsync(c => c.RefreshAsync(new RefreshRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_refresh")
				.Fluent(c => c.Refresh(index))
				.Request(c => c.Refresh(new RefreshRequest(index)))
				.FluentAsync(c => c.RefreshAsync(index))
				.RequestAsync(c => c.RefreshAsync(new RefreshRequest(index)))
				;
		}
	}
}
