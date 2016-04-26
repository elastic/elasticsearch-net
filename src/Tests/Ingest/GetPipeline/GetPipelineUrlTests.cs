using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.GetPipeline
{
	public class GetPipelineUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "id";

			await GET($"/_ingest/pipeline/{id}")
				.Fluent(c => c.GetPipeline(id))
				.Request(c => c.GetPipeline(new GetPipelineRequest(id)))
				.FluentAsync(c => c.GetPipelineAsync(id))
				.RequestAsync(c => c.GetPipelineAsync(new GetPipelineRequest(id)))
				;
		}
	}
}
