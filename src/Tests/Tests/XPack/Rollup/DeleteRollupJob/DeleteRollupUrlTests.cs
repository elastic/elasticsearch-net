using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.DeleteRollupJob
{
	public class DeleteRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await DELETE($"/_rollup/job/{id}")
				.Fluent(c => c.Rollup.DeleteRollupJob(id))
				.Request(c => c.Rollup.DeleteRollupJob(new DeleteRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.DeleteRollupJobAsync(id))
				.RequestAsync(c => c.Rollup.DeleteRollupJobAsync(new DeleteRollupJobRequest(id)));
		}
	}
}
