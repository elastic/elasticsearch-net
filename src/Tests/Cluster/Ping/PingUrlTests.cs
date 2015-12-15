using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.Ping
{
	public class PingUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await HEAD("/")
				.Fluent(c => c.Ping())
				.Request(c => c.Ping(new PingRequest()))
				.FluentAsync(c => c.PingAsync())
				.RequestAsync(c => c.PingAsync(new PingRequest()))
				;
		}
	}
}
