// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Single.TermVectors
{
	public class TermVectorsUrlTests
	{
		[U]
		public async Task Urls()
		{
			var id = "name-of-doc";
			var index = "myindex";

			await GET($"/{index}/_termvectors/{id}")
					.Fluent(c => c.TermVectors<Project>(t => t.Index(index).Id(id)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(index, id)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Index(index).Id(id)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(index, id)))
				;

			await GET($"/project/_termvectors/{id}")
					.Fluent(c => c.TermVectors<Project>(t => t.Id(id)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>((Id)id)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Id(id)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>((Id)id)))
				;

			await GET($"/{index}/_termvectors/{id}")
					.Fluent(c => c.TermVectors<Project>(t => t.Index(index).Id(id)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(index, id)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Index(index).Id(id)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(index, id)))
				;

			var document = new Project { Name = "foo" };

			await POST($"/{index}/_termvectors")
					.Fluent(c => c.TermVectors<Project>(t => t.Index(index).Document(document)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(document, index)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Index(index).Document(document)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(document, index)))
				;

			await POST($"/project/_termvectors")
					.Fluent(c => c.TermVectors<Project>(t => t.Document(document)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(document)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Document(document)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(document)))
				;
		}
	}
}
