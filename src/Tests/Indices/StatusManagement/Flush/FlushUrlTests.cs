using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.Flush
{
	public class FlushUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_flush")
				.Fluent(c => c.Flush(All))
				.Request(c => c.Flush(new FlushRequest()))
				.FluentAsync(c => c.FlushAsync(All))
				.RequestAsync(c => c.FlushAsync(new FlushRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_flush")
				.Fluent(c => c.Flush(index))
				.Request(c => c.Flush(new FlushRequest(index)))
				.FluentAsync(c => c.FlushAsync(index))
				.RequestAsync(c => c.FlushAsync(new FlushRequest(index)))
				;
		}
	}
}
