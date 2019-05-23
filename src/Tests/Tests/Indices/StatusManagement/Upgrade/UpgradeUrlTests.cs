using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
			await POST($"/_all/_upgrade")
					.Fluent(c => c.Indices.Upgrade(All))
					.Request(c => c.Indices.Upgrade(new UpgradeRequest(All)))
					.FluentAsync(c => c.Indices.UpgradeAsync(All))
					.RequestAsync(c => c.Indices.UpgradeAsync(new UpgradeRequest(All)))
				;
			
			await POST($"/_upgrade")
					.Request(c => c.Indices.Upgrade(new UpgradeRequest()))
					.RequestAsync(c => c.Indices.UpgradeAsync(new UpgradeRequest()))
				;

			var index = "index1,index2";
			await POST($"/index1%2Cindex2/_upgrade")
					.Fluent(c => c.Indices.Upgrade(index))
					.Request(c => c.Indices.Upgrade(new UpgradeRequest(index)))
					.FluentAsync(c => c.Indices.UpgradeAsync(index))
					.RequestAsync(c => c.Indices.UpgradeAsync(new UpgradeRequest(index)))
				;
		}
	}
}
