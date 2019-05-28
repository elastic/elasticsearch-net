using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.GetRollupJob
{
	public class GetRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await GET($"/_rollup/job/{id}")
				.Fluent(c => c.Rollup.GetRollupJob(j => j.Id(id)))
				.Request(c => c.Rollup.GetRollupJob(new GetRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.GetRollupJobAsync(j => j.Id(id)))
				.RequestAsync(c => c.Rollup.GetRollupJobAsync(new GetRollupJobRequest(id)));

			await GET($"/_rollup/job/")
				.Fluent(c => c.Rollup.GetRollupJob())
				.Request(c => c.Rollup.GetRollupJob(new GetRollupJobRequest()))
				.FluentAsync(c => c.Rollup.GetRollupJobAsync())
				.RequestAsync(c => c.Rollup.GetRollupJobAsync(new GetRollupJobRequest()));
		}
	}
}
