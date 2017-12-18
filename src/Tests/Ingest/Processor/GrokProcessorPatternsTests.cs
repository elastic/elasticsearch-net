using System.Threading.Tasks;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.Processor
{
	[SkipVersion("<5.6.0","Introduced in Elasticsearch 5.6.0")]
	public class GrokProcessorPatternsUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_ingest/processor/grok")
				.Fluent(c => c.GrokProcessorPatterns())
				.Request(c => c.GrokProcessorPatterns())
				.FluentAsync(c => c.GrokProcessorPatternsAsync())
				.RequestAsync(c => c.GrokProcessorPatternsAsync())
				;
		}
	}
}
