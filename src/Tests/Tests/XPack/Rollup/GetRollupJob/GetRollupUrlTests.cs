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
				.Fluent(c => c.Rollup.GetJob(j => j.Id(id)))
				.Request(c => c.Rollup.GetJob(new GetRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.GetJobAsync(j => j.Id(id)))
				.RequestAsync(c => c.Rollup.GetJobAsync(new GetRollupJobRequest(id)));

			await GET($"/_rollup/job/")
				.Fluent(c => c.Rollup.GetJob())
				.Request(c => c.Rollup.GetJob(new GetRollupJobRequest()))
				.FluentAsync(c => c.Rollup.GetJobAsync())
				.RequestAsync(c => c.Rollup.GetJobAsync(new GetRollupJobRequest()));
		}
	}
}
