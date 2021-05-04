// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.CleanupRepository
{
	public class CleanupRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repos = "repos1";

			await POST($"/_snapshot/repos1/_cleanup")
					.Fluent(c => c.Snapshot.CleanupRepository(repos))
					.Request(c => c.Snapshot.CleanupRepository(new CleanupRepositoryRequest(repos)))
					.FluentAsync(c => c.Snapshot.CleanupRepositoryAsync(repos))
					.RequestAsync(c => c.Snapshot.CleanupRepositoryAsync(new CleanupRepositoryRequest(repos)))
				;
		}
	}
}
