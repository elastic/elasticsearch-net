using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Explain
{
	public class ExplainUrlTests
	{
		[U] public async Task Urls()
		{
			var project = new Project { Name = "NEST" };
			var routing = Infer.Route(project);

			await POST("/project/_explain/NEST?routing=NEST")
					.Fluent(c => c.Explain<Project>(project, e => e.Routing(routing).Query(q => q.MatchAll())))
					.Request(c => c.Explain(new ExplainRequest<Project>(project) { Routing = routing }))
					.FluentAsync(c => c.ExplainAsync<Project>(project, e => e.Routing(routing).Query(q => q.MatchAll())))
					.RequestAsync(c => c.ExplainAsync(new ExplainRequest<Project>(project) { Routing = routing }))
				;

			await POST("/project/_explain/NEST")
					.Fluent(c => c.Explain<Project>("NEST", e => e.Query(q => q.MatchAll())))
					.Request(c => c.Explain(new ExplainRequest<Project>("project", "NEST") { }))
					.FluentAsync(c => c.ExplainAsync<Project>("NEST", e => e.Query(q => q.MatchAll())))
					.RequestAsync(c => c.ExplainAsync(new ExplainRequest<Project>("NEST")))
				;
		}
	}
}
