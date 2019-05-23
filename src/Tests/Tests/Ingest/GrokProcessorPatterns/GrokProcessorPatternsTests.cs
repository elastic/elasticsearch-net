using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.Processor
{
	public class GrokProcessorPatternsUrlTests
	{
		[U] public async Task Urls() => await GET($"/_ingest/processor/grok")
			.Fluent(c => c.Ingest.GrokProcessorPatterns())
			.Request(c => c.Ingest.GrokProcessorPatterns())
			.FluentAsync(c => c.Ingest.GrokProcessorPatternsAsync())
			.RequestAsync(c => c.Ingest.GrokProcessorPatternsAsync());
	}
}
