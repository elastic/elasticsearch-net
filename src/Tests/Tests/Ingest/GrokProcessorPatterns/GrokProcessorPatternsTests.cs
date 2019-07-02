using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework.EndpointTests;

namespace Tests.Ingest.GrokProcessorPatterns
{
	public class GrokProcessorPatternsUrlTests
	{
		[U] public async Task Urls() => await UrlTester.GET($"/_ingest/processor/grok")
			.Fluent(c => c.Ingest.GrokProcessorPatterns())
			.Request(c => c.Ingest.GrokProcessorPatterns())
			.FluentAsync(c => c.Ingest.GrokProcessorPatternsAsync())
			.RequestAsync(c => c.Ingest.GrokProcessorPatternsAsync());
	}
}
