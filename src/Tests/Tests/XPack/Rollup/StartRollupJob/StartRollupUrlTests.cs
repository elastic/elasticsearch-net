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
				.Fluent(c => c.StartRollupJob(id))
				.Request(c => c.StartRollupJob(new StartRollupJobRequest(id)))
				.FluentAsync(c => c.StartRollupJobAsync(id))
				.RequestAsync(c => c.StartRollupJobAsync(new StartRollupJobRequest(id)));
		}
	}
}
