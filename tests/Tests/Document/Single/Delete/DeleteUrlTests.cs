// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Single.Delete
{
	public class DeleteUrlTests
	{
		[U] public async Task Urls() => await DELETE("/project/_doc/1")
			.Fluent(c => c.Delete<Project>(1))
			.Request(c => c.Delete(new DeleteRequest<Project>(1)))
			.FluentAsync(c => c.DeleteAsync<Project>(1))
			.RequestAsync(c => c.DeleteAsync(new DeleteRequest<Project>(1)));
	}
}
