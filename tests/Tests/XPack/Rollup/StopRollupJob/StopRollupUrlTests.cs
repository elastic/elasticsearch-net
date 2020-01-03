using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Rollup.StopRollupJob
{
	public class StopRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await POST($"/_rollup/job/{id}/_stop")
				.Fluent(c => c.Rollup.StopJob(id))
				.Request(c => c.Rollup.StopJob(new StopRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.StopJobAsync(id))
				.RequestAsync(c => c.Rollup.StopJobAsync(new StopRollupJobRequest(id)));
		}
	}
}
