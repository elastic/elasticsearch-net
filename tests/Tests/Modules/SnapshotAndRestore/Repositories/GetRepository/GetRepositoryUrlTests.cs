// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repositories = "repos1,repos2";

			await GET($"/_snapshot/repos1%2Crepos2")
					.Fluent(c => c.Snapshot.GetRepository(s => s.RepositoryName(repositories)))
					.Request(c => c.Snapshot.GetRepository(new GetRepositoryRequest(repositories)))
					.FluentAsync(c => c.Snapshot.GetRepositoryAsync(s => s.RepositoryName(repositories)))
					.RequestAsync(c => c.Snapshot.GetRepositoryAsync(new GetRepositoryRequest(repositories)))
				;
			await GET($"/_snapshot")
					.Fluent(c => c.Snapshot.GetRepository())
					.Request(c => c.Snapshot.GetRepository(new GetRepositoryRequest()))
					.FluentAsync(c => c.Snapshot.GetRepositoryAsync())
					.RequestAsync(c => c.Snapshot.GetRepositoryAsync(new GetRepositoryRequest()))
				;
		}
	}
}
