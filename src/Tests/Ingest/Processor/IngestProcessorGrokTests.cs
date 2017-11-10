using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.Processor
{
	public class IngestProcessorGrokUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_ingest/processor/grok")
				.Fluent(c => c.IngestProcessorGrok())
				.Request(c => c.IngestProcessorGrok())
				.FluentAsync(c => c.IngestProcessorGrokAsync())
				.RequestAsync(c => c.IngestProcessorGrokAsync())
				;
		}
	}
}
