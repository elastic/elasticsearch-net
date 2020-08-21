// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.SnapshotStatus
{
	public class SnapshotStatusUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_snapshot/_status")
					.Fluent(c => c.Snapshot.Status())
					.Request(c => c.Snapshot.Status(new SnapshotStatusRequest()))
					.FluentAsync(c => c.Snapshot.StatusAsync())
					.RequestAsync(c => c.Snapshot.StatusAsync(new SnapshotStatusRequest()))
				;

			var repository = "repos";
			await GET($"/_snapshot/{repository}/_status")
					.Fluent(c => c.Snapshot.Status(s => s.RepositoryName(repository)))
					.Request(c => c.Snapshot.Status(new SnapshotStatusRequest(repository)))
					.FluentAsync(c => c.Snapshot.StatusAsync(s => s.RepositoryName(repository)))
					.RequestAsync(c => c.Snapshot.StatusAsync(new SnapshotStatusRequest(repository)))
				;
			var snapshot = "snap";
			await GET($"/_snapshot/{repository}/{snapshot}/_status")
					.Fluent(c => c.Snapshot.Status(s => s.RepositoryName(repository).Snapshot(snapshot)))
					.Request(c => c.Snapshot.Status(new SnapshotStatusRequest(repository, snapshot)))
					.FluentAsync(c => c.Snapshot.StatusAsync(s => s.RepositoryName(repository).Snapshot(snapshot)))
					.RequestAsync(c => c.Snapshot.StatusAsync(new SnapshotStatusRequest(repository, snapshot)))
				;
		}
	}
}
