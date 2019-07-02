using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Rollup.CreateRollupJob
{
	public class CreateRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await PUT($"/_rollup/job/{id}")
				.Fluent(c => c.Rollup.CreateJob<Project>(id, s => s))
				.Request(c => c.Rollup.CreateJob(new CreateRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.CreateJobAsync<Project>(id, s => s))
				.RequestAsync(c => c.Rollup.CreateJobAsync(new CreateRollupJobRequest(id)));
		}
	}
}
