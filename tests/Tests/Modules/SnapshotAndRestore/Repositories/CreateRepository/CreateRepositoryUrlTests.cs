// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Modules.SnapshotAndRestore.Repositories.CreateRepository
{
	public class CreateRepositoryUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";

			await PUT($"/_snapshot/{repository}")
					.Fluent(c => c.Snapshot.CreateRepository(repository, s => s))
					.Request(c => c.Snapshot.CreateRepository(new CreateRepositoryRequest(repository)))
					.FluentAsync(c => c.Snapshot.CreateRepositoryAsync(repository, s => s))
					.RequestAsync(c => c.Snapshot.CreateRepositoryAsync(new CreateRepositoryRequest(repository)))
				;
		}
	}
}
