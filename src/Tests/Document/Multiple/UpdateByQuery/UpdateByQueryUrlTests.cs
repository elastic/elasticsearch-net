using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;
using static Nest.Infer;

namespace Tests.Document.Multiple.UpdateByQuery
{
	public class UpdateByQueryUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/project/_update_by_query")
				.Fluent(c => c.UpdateByQuery<Project>(d => d.AllTypes()))
				.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project")))
				.FluentAsync(c => c.UpdateByQueryAsync<Project>(d => d.AllTypes()))
				.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project")))
				;

			await POST("/project/doc/_update_by_query")
				.Fluent(c => c.UpdateByQuery<Project>(d => d))
				.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project", "doc")))
				.FluentAsync(c => c.UpdateByQueryAsync<Project>(d => d))
				.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project", "doc")))
				;
		}
	}
}
