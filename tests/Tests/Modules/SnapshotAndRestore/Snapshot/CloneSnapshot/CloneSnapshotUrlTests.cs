// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Snapshot.CloneSnapshot
{
	public class CloneSnapshotUrlTests
	{
		[U] public async Task Urls()
		{
			const string repository = "repository";
			const string snapshot = "snapshot";
			const string target = "snapshot-clone";

			await PUT($"/_snapshot/{repository}/{snapshot}/_clone/{target}")
					.Fluent(c => c.Snapshot.Clone(repository, snapshot, target, f => f))
					.Request(c => c.Snapshot.Clone(new CloneSnapshotRequest(repository, snapshot, target)))
					.FluentAsync(c => c.Snapshot.CloneAsync(repository, snapshot, target, f => f))
					.RequestAsync(c => c.Snapshot.CloneAsync(new CloneSnapshotRequest(repository, snapshot, target)))
				;
		}
	}
}
