using System.Threading.Tasks;
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
				.Fluent(c => c.UpdateByQuery<Project>("project", Types.All,d => d))
				.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project")))
				.FluentAsync(c => c.UpdateByQueryAsync<Project>("project", Types.All, d => d))
				.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project")))
				;

			await POST("/project/project/_update_by_query")
				.Fluent(c => c.UpdateByQuery<Project>("project", Type<Project>(), d=>d))
				.Request(c => c.UpdateByQuery(new UpdateByQueryRequest<Project>("project", "project")))
				.FluentAsync(c => c.UpdateByQueryAsync<Project>(Index<Project>(), Type<Project>(), d=>d))
				.RequestAsync(c => c.UpdateByQueryAsync(new UpdateByQueryRequest<Project>("project", "project")))
				;
		}
	}
}
