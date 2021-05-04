// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Rollup.DeleteRollupJob
{
	public class DeleteRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await DELETE($"/_rollup/job/{id}")
				.Fluent(c => c.Rollup.DeleteJob(id))
				.Request(c => c.Rollup.DeleteJob(new DeleteRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.DeleteJobAsync(id))
				.RequestAsync(c => c.Rollup.DeleteJobAsync(new DeleteRollupJobRequest(id)));
		}
	}
}
