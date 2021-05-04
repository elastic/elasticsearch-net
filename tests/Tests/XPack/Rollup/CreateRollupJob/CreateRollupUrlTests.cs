// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Rollup.CreateRollupJob
{
	public class CreateRollupUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			const string id = "rollup-id";
			await PUT($"/_rollup/job/{id}")
				.Fluent(c => c.Rollup.CreateJob<Project>(id, s => s))
				.Request(c => c.Rollup.CreateJob(new CreateRollupJobRequest(id)))
				.FluentAsync(c => c.Rollup.CreateJobAsync<Project>(id, s => s))
				.RequestAsync(c => c.Rollup.CreateJobAsync(new CreateRollupJobRequest(id)));
		}
	}
}
