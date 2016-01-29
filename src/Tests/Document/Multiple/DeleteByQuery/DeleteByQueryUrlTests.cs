using System.Threading.Tasks;
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
			await DELETE("/project/_query")
				.Fluent(c => c.DeleteByQuery<Project>("project", Types.All,d => d))
				.Request(c => c.DeleteByQuery(new DeleteByQueryRequest<Project>("project")))
				.FluentAsync(c => c.DeleteByQueryAsync<Project>("project", Types.All, d => d))
				.RequestAsync(c => c.DeleteByQueryAsync(new DeleteByQueryRequest<Project>("project")))
				;

			await DELETE("/project/project/_query")
				.Fluent(c => c.DeleteByQuery<Project>("project", Type<Project>(), d=>d))
				.Request(c => c.DeleteByQuery(new DeleteByQueryRequest<Project>("project", "project")))
				.FluentAsync(c => c.DeleteByQueryAsync<Project>(Index<Project>(), Type<Project>(), d=>d))
				.RequestAsync(c => c.DeleteByQueryAsync(new DeleteByQueryRequest<Project>("project", "project")))
				;
		}
	}
}
