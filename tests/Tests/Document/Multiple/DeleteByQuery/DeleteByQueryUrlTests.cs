// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.DeleteByQuery
{
	public class DeleteByQueryUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/project/_delete_by_query")
				.Fluent(c => c.DeleteByQuery<Project>("project", d => { }))
				.Request(c => c.DeleteByQuery(new DeleteByQueryRequest("project")))
				.FluentAsync(c => c.DeleteByQueryAsync<Project>("project", d => { }))
				.RequestAsync(c => c.DeleteByQueryAsync(new DeleteByQueryRequest("project")));
	}
}
