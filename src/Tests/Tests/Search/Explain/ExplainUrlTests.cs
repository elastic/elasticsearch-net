using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Explain
{
	public class ExplainUrlTests
	{
		[U] public async Task Urls()
		{
			var project = new Project { Name = "NEST" };

			await POST("/project/doc/NEST/_explain?routing=NEST")
				.Fluent(c => c.Explain<Project>(project, e => e.Query(q=>q.MatchAll())))
				.Request(c => c.Explain(new ExplainRequest<Project>(project) {}))
				.FluentAsync(c => c.ExplainAsync<Project>(project, e=>e.Query(q=>q.MatchAll())))
				.RequestAsync(c => c.ExplainAsync(new ExplainRequest<Project>(project)))
				;

			await POST("/project/doc/NEST/_explain")
				.Fluent(c => c.Explain<Project>("NEST", e => e.Query(q=>q.MatchAll())))
				.Request(c => c.Explain(new ExplainRequest<Project>("project", "doc", "NEST") {}))
				.FluentAsync(c => c.ExplainAsync<Project>("NEST", e=>e.Query(q=>q.MatchAll())))
				.RequestAsync(c => c.ExplainAsync(new ExplainRequest<Project>("NEST")))
				;
		}
	}
}
