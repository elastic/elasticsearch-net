using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Multiple.ReindexOnServer
{
	public class ReindexOnServerUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_reindex")
				.Fluent(c => c.ReindexOnServer(f=>f))
				.Request(c => c.ReindexOnServer(new ReindexOnServerRequest()))
				.FluentAsync(c => c.ReindexOnServerAsync(f=>f))
				.RequestAsync(c => c.ReindexOnServerAsync(new ReindexOnServerRequest()))
				;

		}
	}
}
