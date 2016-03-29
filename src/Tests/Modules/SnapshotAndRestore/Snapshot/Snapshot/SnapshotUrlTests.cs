using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.Snapshot
{
	public class SnapshotUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await PUT($"/_snapshot/{repository}/{snapshot}")
				.Fluent(c => c.Snapshot(repository, snapshot))
				.Request(c => c.Snapshot(new SnapshotRequest(repository, snapshot)))
				.FluentAsync(c => c.SnapshotAsync(repository, snapshot))
				.RequestAsync(c => c.SnapshotAsync(new SnapshotRequest(repository, snapshot)))
				;
		}
	}
}
