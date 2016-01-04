using System.Threading.Tasks;
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
			var project = new Project { Name = "NEST" };

			await POST("/project/project")
				.Fluent(c => c.Index(project, i => i.Id(null)))
				.Request(c => c.Index(new IndexRequest<Project>("project", "project") { Document = project }))
				.FluentAsync(c => c.IndexAsync(project, i => i.Id(null)))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(typeof(Project), TypeName.From<Project>())
                {
					Document = project
				}))
				;

			await PUT("/project/project/NEST")
				.Fluent(c => c.Index(project))
				.Request(c => c.Index(new IndexRequest<Project>("project", "project", "NEST") { Document = project }))
				.Request(c => c.Index(new IndexRequest<Project>(project)))
				.FluentAsync(c => c.IndexAsync(project))
				.RequestAsync(c => c.IndexAsync(new IndexRequest<Project>(project)))
				;
		}
	}
}
