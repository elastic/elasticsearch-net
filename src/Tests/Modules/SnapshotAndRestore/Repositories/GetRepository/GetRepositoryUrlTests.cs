using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repositories = "repos1,repos2";

			await GET($"/_snapshot/repos1%2Crepos2")
				.Fluent(c => c.GetRepository(s=>s.RepositoryName(repositories)))
				.Request(c => c.GetRepository(new GetRepositoryRequest(repositories)))
				.FluentAsync(c => c.GetRepositoryAsync(s=>s.RepositoryName(repositories)))
				.RequestAsync(c => c.GetRepositoryAsync(new GetRepositoryRequest(repositories)))
				;
			await GET($"/_snapshot")
				.Fluent(c => c.GetRepository())
				.Request(c => c.GetRepository(new GetRepositoryRequest()))
				.FluentAsync(c => c.GetRepositoryAsync())
				.RequestAsync(c => c.GetRepositoryAsync(new GetRepositoryRequest()))
				;
		}
	}
}
