using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;
using static Nest_5_2_0.Infer;

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

			await POST("/project/project/_update_by_query")
				.Fluent(c => c.UpdateByQuery<Project>(d => d))
				.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project", "project")))
				.FluentAsync(c => c.UpdateByQueryAsync<Project>(d => d))
				.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project", "project")))
				;
		}
	}
}
