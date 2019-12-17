using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.VerifyRepository
{
	public class VerifyRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repos = "repos1";

			await POST($"/_snapshot/repos1/_verify")
					.Fluent(c => c.Snapshot.VerifyRepository(repos))
					.Request(c => c.Snapshot.VerifyRepository(new VerifyRepositoryRequest(repos)))
					.FluentAsync(c => c.Snapshot.VerifyRepositoryAsync(repos))
					.RequestAsync(c => c.Snapshot.VerifyRepositoryAsync(new VerifyRepositoryRequest(repos)))
				;
		}
	}
}
