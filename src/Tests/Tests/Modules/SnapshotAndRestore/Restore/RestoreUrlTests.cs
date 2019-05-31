using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Restore.Restore
{
	public class RestoreUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await POST($"/_snapshot/{repository}/{snapshot}/_restore")
					.Fluent(c => c.Snapshot.Restore(repository, snapshot))
					.Request(c => c.Snapshot.Restore(new RestoreRequest(repository, snapshot)))
					.FluentAsync(c => c.Snapshot.RestoreAsync(repository, snapshot))
					.RequestAsync(c => c.Snapshot.RestoreAsync(new RestoreRequest(repository, snapshot)))
				;
		}
	}
}
