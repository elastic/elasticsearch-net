using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Single.Index
{
	public class IndexUrlTests
	{
		[U] public async Task Urls()
		{
			var project = new Project {Name = "NEST"};

			await POST("/project/doc?routing=NEST")
				.Fluent(c => c.Index(project, i => i.Id(null)))
				.Request(c => c.Index(new IndexRequest<Project>("project", "doc") {Document = project}))
				.FluentAsync(c => c.IndexAsync(project, i => i.Id(null)))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(typeof(Project), TypeName.From<Project>())
				{
					Document = project
				}));

			//no explicit ID is provided and none can be inferred on the anonymous object so this falls back to a POST to /index/type
			await POST("/project/doc")
				.Fluent(c => c.Index(new { }, i => i.Index(typeof(Project)).Type(typeof(Project))))
				.Request(c => c.Index(new IndexRequest<object>("project", "doc") {Document = new { }}))
				.FluentAsync(c => c.IndexAsync(new { }, i => i.Index(typeof(Project)).Type(typeof(Project))))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<object>(typeof(Project), TypeName.From<Project>())
				{
					Document = new { }
				}));

			await PUT("/project/doc/NEST?routing=NEST")
				.Fluent(c => c.IndexDocument(project))
				.Request(c => c.Index(new IndexRequest<Project>("project", "doc", "NEST") {Document = project}))
				.Request(c => c.Index(new IndexRequest<Project>(project)))
				.FluentAsync(c => c.IndexDocumentAsync(project))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(project))) ;
		}

		[U] public async Task CanIndexUrlIds()
		{
			var id = "http://my.local/id?qwe=2";
			var escaped = Uri.EscapeDataString(id);
			escaped.Should().NotContain("/").And.NotContain("?");
			var project = new Project {Name = "name"};

			await PUT($"/project/doc/{escaped}?routing=name")
				.Fluent(c => c.Index(project, i => i.Id(id)))
				.Request(c => c.Index(new IndexRequest<Project>("project", "doc", id) {Document = project}))
				.FluentAsync(c => c.IndexAsync(project, i => i.Id(id)))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(typeof(Project), TypeName.From<Project>(), id)
				{
					Document = project
				}));

			project = new Project {Name = id};
			await PUT($"/project/doc/{escaped}?routing={escaped}")
				.Fluent(c => c.Index(project, i => i.Id(id)))
				.Request(c => c.Index(new IndexRequest<Project>("project", "doc", id) {Document = project}))
				.FluentAsync(c => c.IndexAsync(project, i => i.Id(id)))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(typeof(Project), TypeName.From<Project>(), id)
				{
					Document = project
				}));
		}
	}
}
