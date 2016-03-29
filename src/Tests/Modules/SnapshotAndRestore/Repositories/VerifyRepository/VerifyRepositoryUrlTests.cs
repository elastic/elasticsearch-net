using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.VerifyRepository
{
	public class VerifyRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repos = "repos1";

			await POST($"/_snapshot/repos1/_verify")
				.Fluent(c => c.VerifyRepository(repos))
				.Request(c => c.VerifyRepository(new VerifyRepositoryRequest(repos)))
				.FluentAsync(c => c.VerifyRepositoryAsync(repos))
				.RequestAsync(c => c.VerifyRepositoryAsync(new VerifyRepositoryRequest(repos)))
				;
		}
	}
}
