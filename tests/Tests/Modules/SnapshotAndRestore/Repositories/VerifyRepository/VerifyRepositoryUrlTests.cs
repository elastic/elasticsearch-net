// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.VerifyRepository
{
	public class VerifyRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repos = "repos1";

			await POST($"/_snapshot/repos1/_verify")
					.Fluent(c => c.Snapshot.VerifyRepository(repos))
					.Request(c => c.Snapshot.VerifyRepository(new VerifyRepositoryRequest(repos)))
					.FluentAsync(c => c.Snapshot.VerifyRepositoryAsync(repos))
					.RequestAsync(c => c.Snapshot.VerifyRepositoryAsync(new VerifyRepositoryRequest(repos)))
				;
		}
	}
}
