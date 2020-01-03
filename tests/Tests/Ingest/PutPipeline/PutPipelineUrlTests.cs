using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Ingest.PutPipeline
{
	public class PutPipelineUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "id";

			await PUT($"/_ingest/pipeline/{id}")
					.Fluent(c => c.Ingest.PutPipeline(id, s => s))
					.Request(c => c.Ingest.PutPipeline(new PutPipelineRequest(id)))
					.FluentAsync(c => c.Ingest.PutPipelineAsync(id, s => s))
					.RequestAsync(c => c.Ingest.PutPipelineAsync(new PutPipelineRequest(id)))
				;
		}
	}
}
