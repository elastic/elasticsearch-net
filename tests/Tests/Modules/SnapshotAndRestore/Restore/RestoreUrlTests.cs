// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.Modules.SnapshotAndRestore.Restore
{
	public class RestoreUrlTests
	{
		[U] public async Task Urls()
		{
			var repository = "repos";
			var snapshot = "snap";

			await UrlTester.POST($"/_snapshot/{repository}/{snapshot}/_restore")
					.Fluent(c => c.Snapshot.Restore(repository, snapshot))
					.Request(c => c.Snapshot.Restore(new RestoreRequest(repository, snapshot)))
					.FluentAsync(c => c.Snapshot.RestoreAsync(repository, snapshot))
					.RequestAsync(c => c.Snapshot.RestoreAsync(new RestoreRequest(repository, snapshot)))
				;
		}
	}
}
