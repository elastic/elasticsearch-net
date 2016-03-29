using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Single.Delete
{
	public class DeleteUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/project/project/1")
				.Fluent(c => c.Delete<Project>(1))
				.Request(c => c.Delete(new DeleteRequest<Project>(1)))
				.FluentAsync(c => c.DeleteAsync<Project>(1))
				.RequestAsync(c => c.DeleteAsync(new DeleteRequest<Project>(1)))
				;
		}
	}
}
