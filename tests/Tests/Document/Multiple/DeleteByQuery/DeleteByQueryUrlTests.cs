// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.DeleteByQuery
{
	public class DeleteByQueryUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/project/_delete_by_query")
				.Fluent(c => c.DeleteByQuery<Project>(d => d))
				.Request(c => c.DeleteByQuery(new DeleteByQueryRequest<Project>("project")))
				.FluentAsync(c => c.DeleteByQueryAsync<Project>(d => d))
				.RequestAsync(c => c.DeleteByQueryAsync(new DeleteByQueryRequest<Project>("project")));
	}
}
