using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.DeletePipeline
{
	public class DeletePipelineUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "pipeline-1";

			await DELETE($"/_ingest/pipeline/{id}")
				.Fluent(c => c.DeletePipeline(id))
				.Request(c => c.DeletePipeline(new DeletePipelineRequest(id)))
				.FluentAsync(c => c.DeletePipelineAsync(id))
				.RequestAsync(c => c.DeletePipelineAsync(new DeletePipelineRequest(id)))
				;
		}
	}
}
