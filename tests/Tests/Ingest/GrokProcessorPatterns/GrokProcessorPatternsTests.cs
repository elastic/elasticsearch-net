// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
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
