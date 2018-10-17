using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Ingest.Processor
{
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
