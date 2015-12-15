using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.SnapshotStatus
{
	public class SnapshotStatusUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_snapshot/_status")
				.Fluent(c => c.SnapshotStatus())
				.Request(c => c.SnapshotStatus(new SnapshotStatusRequest()))
				.FluentAsync(c => c.SnapshotStatusAsync())
				.RequestAsync(c => c.SnapshotStatusAsync(new SnapshotStatusRequest()))
				;

			var repository = "repos";
			await GET($"/_snapshot/{repository}/_status")
				.Fluent(c => c.SnapshotStatus(s=>s.RepositoryName(repository)))
				.Request(c => c.SnapshotStatus(new SnapshotStatusRequest(repository)))
				.FluentAsync(c => c.SnapshotStatusAsync(s=>s.RepositoryName(repository)))
				.RequestAsync(c => c.SnapshotStatusAsync(new SnapshotStatusRequest(repository)))
				;
			var snapshot = "snap";
			await GET($"/_snapshot/{repository}/{snapshot}/_status")
				.Fluent(c => c.SnapshotStatus(s=>s.RepositoryName(repository).Snapshot(snapshot)))
				.Request(c => c.SnapshotStatus(new SnapshotStatusRequest(repository, snapshot)))
				.FluentAsync(c => c.SnapshotStatusAsync(s=>s.RepositoryName(repository).Snapshot(snapshot)))
				.RequestAsync(c => c.SnapshotStatusAsync(new SnapshotStatusRequest(repository, snapshot)))
				;
		}
	}
}
