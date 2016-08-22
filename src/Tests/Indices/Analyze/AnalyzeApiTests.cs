using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.Analyze
{
	public class AnalyzeApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IAnalyzeResponse, IAnalyzeRequest, AnalyzeDescriptor, AnalyzeRequest>
	{
		public AnalyzeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Analyze(f),
			fluentAsync: (client, f) => client.AnalyzeAsync(f),
			request: (client, r) => client.Analyze(r),
			requestAsync: (client, r) => client.AnalyzeAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_analyze";

		protected override object ExpectJson => new
		{
			text = new[] { "hello world", "domination" },
			char_filter = new[] { "html_strip" },
			tokenizer = "keyword",
			filter = new[] { "lowercase", "stop" }
		};

		protected override Func<AnalyzeDescriptor, IAnalyzeRequest> Fluent => d => d
			.Text("hello world", "domination")
			.CharFilter("html_strip")
			.Tokenizer("keyword")
			.Filter("lowercase", "stop");

		protected override AnalyzeRequest Initializer => new AnalyzeRequest
		{
			Text = new[] { "hello world", "domination" },
			CharFilter = new[] { "html_strip" },
			Tokenizer = "keyword",
			Filter = new[] { "lowercase", "stop" }
		};
	}
}
