// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.Snapshot
{
	public class SnapshotUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await PUT($"/_snapshot/{repository}/{snapshot}")
					.Fluent(c => c.Snapshot.Snapshot(repository, snapshot))
					.Request(c => c.Snapshot.Snapshot(new SnapshotRequest(repository, snapshot)))
					.FluentAsync(c => c.Snapshot.SnapshotAsync(repository, snapshot))
					.RequestAsync(c => c.Snapshot.SnapshotAsync(new SnapshotRequest(repository, snapshot)))
				;


			await ExpectUrl(HttpMethod.PUT, $"/_snapshot/{repository}/{snapshot}?pretty=true", s => s.PrettyJson())
				.Fluent(c => c.Snapshot.Snapshot(repository, snapshot))
				.Request(c => c.Snapshot.Snapshot(new SnapshotRequest(repository, snapshot)))
				.FluentAsync(c => c.Snapshot.SnapshotAsync(repository, snapshot))
				.RequestAsync(c => c.Snapshot.SnapshotAsync(new SnapshotRequest(repository, snapshot)));

			await ExpectUrl(HttpMethod.PUT, $"/_snapshot/{repository}/{snapshot}?pretty=true", s => s.PrettyJson())
				.Fluent(c => c.Snapshot.Snapshot(repository, snapshot, s => s.Pretty()))
				.Request(c => c.Snapshot.Snapshot(new SnapshotRequest(repository, snapshot) { Pretty = true }))
				.FluentAsync(c => c.Snapshot.SnapshotAsync(repository, snapshot, s => s.Pretty()))
				.RequestAsync(c => c.Snapshot.SnapshotAsync(new SnapshotRequest(repository, snapshot) { Pretty = true }));
		}
	}
}
