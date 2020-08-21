// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.DeleteSnapshot
{
	public class DeleteSnapshotUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await DELETE($"/_snapshot/{repository}/{snapshot}")
					.Fluent(c => c.Snapshot.Delete(repository, snapshot))
					.Request(c => c.Snapshot.Delete(new DeleteSnapshotRequest(repository, snapshot)))
					.FluentAsync(c => c.Snapshot.DeleteAsync(repository, snapshot))
					.RequestAsync(c => c.Snapshot.DeleteAsync(new DeleteSnapshotRequest(repository, snapshot)))
				;
		}
	}
}
