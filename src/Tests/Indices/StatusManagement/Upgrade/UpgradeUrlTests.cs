using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.StatusManagement.Upgrade
{
	public class UpgradeUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_upgrade")
				.Fluent(c => c.Upgrade(All))
				.Request(c => c.Upgrade(new UpgradeRequest()))
				.FluentAsync(c => c.UpgradeAsync(All))
				.RequestAsync(c => c.UpgradeAsync(new UpgradeRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_upgrade")
				.Fluent(c => c.Upgrade(index))
				.Request(c => c.Upgrade(new UpgradeRequest(index)))
				.FluentAsync(c => c.UpgradeAsync(index))
				.RequestAsync(c => c.UpgradeAsync(new UpgradeRequest(index)))
				;
		}
	}
}
