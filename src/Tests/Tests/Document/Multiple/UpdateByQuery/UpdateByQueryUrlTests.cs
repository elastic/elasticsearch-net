using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

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

			await POST("/project2/doc/_update_by_query")
					.Fluent(c => c.UpdateByQuery<Project>(d => d.Index("project2")))
					.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project2")))
					.FluentAsync(c => c.UpdateByQueryAsync<Project>(d => d.Index("project2")))
					.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project2")))
				;
		}
	}
}
