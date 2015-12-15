using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.DeleteSnapshot
{
	public class DeleteSnapshotUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await DELETE($"/_snapshot/{repository}/{snapshot}")
				.Fluent(c => c.DeleteSnapshot(repository, snapshot))
				.Request(c => c.DeleteSnapshot(new DeleteSnapshotRequest(repository, snapshot)))
				.FluentAsync(c => c.DeleteSnapshotAsync(repository, snapshot))
				.RequestAsync(c => c.DeleteSnapshotAsync(new DeleteSnapshotRequest(repository, snapshot)))
				;
		}
	}
}
