using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Rollup.CreateRollupJob
{
	public class CreateRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await PUT($"/_rollup/job/{id}")
				.Fluent(c => c.CreateRollupJob<Project>(id, s => s))
				.Request(c => c.CreateRollupJob(new CreateRollupJobRequest(id)))
				.FluentAsync(c => c.CreateRollupJobAsync<Project>(id, s => s))
				.RequestAsync(c => c.CreateRollupJobAsync(new CreateRollupJobRequest(id)));
		}
	}
}
