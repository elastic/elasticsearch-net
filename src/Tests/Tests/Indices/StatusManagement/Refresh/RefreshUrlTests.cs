using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.Refresh
{
	public class RefreshUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_refresh")
					.Request(c => c.Refresh(new RefreshRequest()))
					.RequestAsync(c => c.RefreshAsync(new RefreshRequest()))
				;
			
			await POST($"/_all/_refresh")
					.Fluent(c => c.Refresh(All))
					.Request(c => c.Refresh(new RefreshRequest(All)))
					.FluentAsync(c => c.RefreshAsync(All))
					.RequestAsync(c => c.RefreshAsync(new RefreshRequest(All)))
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
