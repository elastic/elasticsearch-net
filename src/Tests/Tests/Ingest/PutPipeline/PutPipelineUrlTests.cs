using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.PutPipeline
{
	public class PutPipelineUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "id";

			await PUT($"/_ingest/pipeline/{id}")
				.Fluent(c => c.PutPipeline(id, s=>s))
				.Request(c => c.PutPipeline(new PutPipelineRequest(id)))
				.FluentAsync(c => c.PutPipelineAsync(id, s=>s))
				.RequestAsync(c => c.PutPipelineAsync(new PutPipelineRequest(id)))
				;
		}
	}
}
