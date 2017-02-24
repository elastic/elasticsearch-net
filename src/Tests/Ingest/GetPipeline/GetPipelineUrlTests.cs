using System.Threading.Tasks;
using Nest_5_2_0;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.GetPipeline
{
	public class GetPipelineUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_ingest/pipeline")
				.Fluent(c => c.GetPipeline())
				.Request(c => c.GetPipeline(new GetPipelineRequest()))
				.FluentAsync(c => c.GetPipelineAsync())
				.RequestAsync(c => c.GetPipelineAsync())
				;

			var id = "id";

			await GET($"/_ingest/pipeline/{id}")
				.Fluent(c => c.GetPipeline(g => g.Id(id)))
				.Request(c => c.GetPipeline(new GetPipelineRequest(id)))
				.FluentAsync(c => c.GetPipelineAsync(g => g.Id(id)))
				.RequestAsync(c => c.GetPipelineAsync(new GetPipelineRequest(id)))
				;
		}
	}
}
