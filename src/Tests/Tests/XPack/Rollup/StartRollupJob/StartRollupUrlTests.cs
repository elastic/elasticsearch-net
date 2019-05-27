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
				.Fluent(c => c.Rollup.StartJob(id))
				.Request(c => c.Rollup.StartJob(new StartRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.StartJobAsync(id))
				.RequestAsync(c => c.Rollup.StartJobAsync(new StartRollupJobRequest(id)));
		}
	}
}
