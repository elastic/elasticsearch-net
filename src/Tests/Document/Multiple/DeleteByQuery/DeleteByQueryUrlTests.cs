using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;
using static Nest.Infer;

namespace Tests.Document.Multiple.DeleteByQuery
{
	public class DeleteByQueryUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/project/_delete_by_query")
				.Fluent(c => c.DeleteByQuery<Project>(d => d.AllTypes()))
				.Request(c => c.DeleteByQuery(new DeleteByQueryRequest<Project>("project")))
				.FluentAsync(c => c.DeleteByQueryAsync<Project>(d => d.AllTypes()))
				.RequestAsync(c => c.DeleteByQueryAsync(new DeleteByQueryRequest<Project>("project")))
				;

			await POST("/project/doc/_delete_by_query")
				.Fluent(c => c.DeleteByQuery<Project>(d => d))
				.Request(c => c.DeleteByQuery(new DeleteByQueryRequest<Project>("project", "doc")))
				.FluentAsync(c => c.DeleteByQueryAsync<Project>(d => d))
				.RequestAsync(c => c.DeleteByQueryAsync(new DeleteByQueryRequest<Project>("project", "doc")))
				;
		}
	}
}
