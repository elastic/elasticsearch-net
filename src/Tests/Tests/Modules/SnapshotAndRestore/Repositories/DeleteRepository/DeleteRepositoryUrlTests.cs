using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.DeleteRepository
{
	public class DeleteRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";

			await DELETE($"/_snapshot/{repository}")
					.Fluent(c => c.Snapshot.DeleteRepository(repository))
					.Request(c => c.Snapshot.DeleteRepository(new DeleteRepositoryRequest(repository)))
					.FluentAsync(c => c.Snapshot.DeleteRepositoryAsync(repository))
					.RequestAsync(c => c.Snapshot.DeleteRepositoryAsync(new DeleteRepositoryRequest(repository)))
				;
		}
	}
}
