using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Document.Single.SourceExists
{
	public class SourceExistsUrlTests
	{
		[U] public async Task Urls()
		{
			await HEAD("/project/project/1/_source")
				.Fluent(c => c.SourceExists<Project>(1))
				.Request(c => c.SourceExists(new SourceExistsRequest<Project>(1)))
				.FluentAsync(c => c.SourceExistsAsync<Project>(1))
				.RequestAsync(c => c.SourceExistsAsync(new SourceExistsRequest<Project>(1)))
				;
		}
	}
}
