// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Single.Index
{
	public class IndexUrlTests
	{
		[U] public async Task Urls()
		{
			var project = new Project { Name = "NEST" };

			await POST("/project/_doc")
				.Fluent(c => c.Index(project, i => i.Id(null)))
				.Request(c => c.Index(new IndexRequest<Project>(index: "project") { Document = project }))
				.FluentAsync(c => c.IndexAsync(project, i => i.Id(null)))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(typeof(Project))
				{
					Document = project
				}));

			//no explicit ID is provided and none can be inferred on the anonymous object so this falls back to a POST to /index/type
			await POST("/project/_doc")
				.Fluent(c => c.Index(new { }, i => i.Index(typeof(Project))))
				.Request(c => c.Index(new IndexRequest<object>(index: "project") { Document = new { } }))
				.FluentAsync(c => c.IndexAsync(new { }, i => i.Index(typeof(Project))))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<object>(typeof(Project))
				{
					Document = new { }
				}));

			await PUT("/project/_doc/NEST")
				.Fluent(c => c.IndexDocument(project))
				.Request(c => c.Index(new IndexRequest<Project>(index: "project", id: "NEST") { Document = project }))
				.Request(c => c.Index(new IndexRequest<Project>(project)))
				.FluentAsync(c => c.IndexDocumentAsync(project))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(project)));
		}

		[U] public async Task LowLevelUrls()
		{
			var project = new Project { Name = "NEST" };

			await POST("/index/_doc")
				.LowLevel(c => c.Index<VoidResponse>("index", PostData.Empty))
				.LowLevelAsync(c => c.IndexAsync<VoidResponse>("index", PostData.Empty));

			await PUT("/index/_doc/id")
				.LowLevel(c => c.Index<VoidResponse>("index", "id", PostData.Empty))
				.LowLevelAsync(c => c.IndexAsync<VoidResponse>("index", "id", PostData.Empty));

		}

		[U] public async Task CanIndexUrlIds()
		{
			var id = "http://my.local/id?qwe=2";
			var escaped = Uri.EscapeDataString(id);
			escaped.Should().NotContain("/").And.NotContain("?");
			var project = new Project { Name = "name" };

			await PUT($"/project/_doc/{escaped}")
				.Fluent(c => c.Index(project, i => i.Id(id)))
				.Request(c => c.Index(new IndexRequest<Project>("project", id) { Document = project }))
				.FluentAsync(c => c.IndexAsync(project, i => i.Id(id)))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(typeof(Project), id)
				{
					Document = project
				}));

			project = new Project { Name = id };
			await PUT($"/project/_doc/{escaped}")
				.Fluent(c => c.Index(project, i => i.Id(id)))
				.Request(c => c.Index(new IndexRequest<Project>("project", id) { Document = project }))
				.FluentAsync(c => c.IndexAsync(project, i => i.Id(id)))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(typeof(Project), id)
				{
					Document = project
				}));
		}
	}
}
