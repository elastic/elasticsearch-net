// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.UpdateByQuery
{
	public class UpdateByQueryUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/project/_update_by_query")
					.Fluent(c => c.UpdateByQuery<Project>(d => d))
					.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project")))
					.FluentAsync(c => c.UpdateByQueryAsync<Project>(d => d))
					.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project")))
				;

			await POST("/project2/_update_by_query")
					.Fluent(c => c.UpdateByQuery<Project>(d => d.Index("project2")))
					.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project2")))
					.FluentAsync(c => c.UpdateByQueryAsync<Project>(d => d.Index("project2")))
					.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project2")))
				;
		}
	}
}
