using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.StopRollupJob
{
	public class StopRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await POST($"/_rollup/job/{id}/_stop")
				.Fluent(c => c.Rollup.StopRollupJob(id))
				.Request(c => c.Rollup.StopRollupJob(new StopRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.StopRollupJobAsync(id))
				.RequestAsync(c => c.Rollup.StopRollupJobAsync(new StopRollupJobRequest(id)));
		}
	}
}
