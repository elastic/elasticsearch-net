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
				.Fluent(c => c.DeleteRollupJob(id))
				.Request(c => c.DeleteRollupJob(new DeleteRollupJobRequest(id)))
				.FluentAsync(c => c.DeleteRollupJobAsync(id))
				.RequestAsync(c => c.DeleteRollupJobAsync(new DeleteRollupJobRequest(id)));
		}
	}
}
