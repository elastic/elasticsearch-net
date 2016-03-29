using System;
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

			var urlId = "http://id.mynamespace/metainformation?x=y,2";
			var escaped = "http%3A%2F%2Fid.mynamespace%2Fmetainformation%3Fx%3Dy%2C2";
			await GET($"/project/project/{escaped}")
				.Fluent(c => c.Get<Project>(urlId))
				.Request(c => c.Get<Project>(new GetRequest<Project>(urlId)))
				.FluentAsync(c => c.GetAsync<Project>(urlId))
				.RequestAsync(c => c.GetAsync<Project>(new GetRequest<Project>(urlId)))
				;

			await GET($"/project/project/{escaped}?routing={escaped}")
				.Fluent(c => c.Get<Project>(urlId, s=>s.Routing(urlId)))
				.Request(c => c.Get<Project>(new GetRequest<Project>(urlId) { Routing = urlId }))
				.FluentAsync(c => c.GetAsync<Project>(urlId, s=>s.Routing(urlId)))
				.RequestAsync(c => c.GetAsync<Project>(new GetRequest<Project>(urlId) { Routing = urlId }))
				;

			GET($"/project/project/{escaped}?routing={escaped}")
				.LowLevel(c => c.Get<dynamic>("project", "project", urlId, s=>s.Routing(urlId)))
				;
		}
	}
}
