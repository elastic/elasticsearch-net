using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;
using static Nest.Infer;

namespace Tests.Document.Single.Get
{
	public class GetUrlTests
	{
		[U]
		public async Task Urls()
		{
			await GET("/project/project/1")
				.Fluent(c => c.Get<Project>(1))
				.Request(c => c.Get<Project>(new GetRequest<Project>(1)))
				.FluentAsync(c => c.GetAsync<Project>(1))
				.RequestAsync(c => c.GetAsync<Project>(new GetRequest<Project>(1)))
				;
			await GET("/testindex/typeindex/1")
				.Fluent(c => c.Get<Project>(1, g => g.Index("testindex").Type("typeindex")))
				.Request(c => c.Get<Project>(new GetRequest<Project>(new DocumentPath<Project>(1).Index("testindex").Type("typeindex"))))
				.Request(c => c.Get<Project>(new GetRequest<Project>("testindex", "typeindex", 1)))
				.FluentAsync(c => c.GetAsync<Project>(1, g => g.Index("testindex").Type("typeindex")))
				.RequestAsync(c => c.GetAsync<Project>(new GetRequest<Project>(new DocumentPath<Project>(1).Index("testindex").Type("typeindex"))))
				.RequestAsync(c => c.GetAsync<Project>(new GetRequest<Project>("testindex", "typeindex", 1)))
				;
		}
	}
}
