using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.StartRollupJob
{
	public class StartRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await POST($"/_rollup/job/{id}/_start")
				.Fluent(c => c.Rollup.StartRollupJob(id))
				.Request(c => c.Rollup.StartRollupJob(new StartRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.StartRollupJobAsync(id))
				.RequestAsync(c => c.Rollup.StartRollupJobAsync(new StartRollupJobRequest(id)));
		}
	}
}
