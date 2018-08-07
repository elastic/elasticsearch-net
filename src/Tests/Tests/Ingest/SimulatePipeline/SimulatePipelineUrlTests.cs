using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.SimulatePipeline
{
	public class SimulatePipelineUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_ingest/pipeline/_simulate")
				.Fluent(c => c.SimulatePipeline(s=>s))
				.Request(c => c.SimulatePipeline(new SimulatePipelineRequest()))
				.FluentAsync(c => c.SimulatePipelineAsync(s=>s))
				.RequestAsync(c => c.SimulatePipelineAsync(new SimulatePipelineRequest()))
				;

			var id = "id";
			await POST($"/_ingest/pipeline/{id}/_simulate")
				.Fluent(c => c.SimulatePipeline(s=>s.Id(id)))
				.Request(c => c.SimulatePipeline(new SimulatePipelineRequest(id)))
				.FluentAsync(c => c.SimulatePipelineAsync(s=>s.Id(id)))
				.RequestAsync(c => c.SimulatePipelineAsync(new SimulatePipelineRequest(id)))
				;
		}
	}
}
