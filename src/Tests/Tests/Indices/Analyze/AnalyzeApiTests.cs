using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.Analyze
{
	public class AnalyzeApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IAnalyzeResponse, IAnalyzeRequest, AnalyzeDescriptor, AnalyzeRequest>
	{
		public AnalyzeApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			text = new[] { "hello world", "domination" },
			char_filter = new[] { "html_strip" },
			tokenizer = "keyword",
			filter = new[] { "lowercase", "stop" }
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<AnalyzeDescriptor, IAnalyzeRequest> Fluent => d => d
			.Text("hello world", "domination")
			.CharFilter("html_strip")
			.Tokenizer("keyword")
			.Filter("lowercase", "stop");

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override AnalyzeRequest Initializer => new AnalyzeRequest
		{
			Text = new[] { "hello world", "domination" },
			CharFilter = new[] { "html_strip" },
			Tokenizer = "keyword",
			Filter = new[] { "lowercase", "stop" }
		};

		protected override string UrlPath => $"/_analyze";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Analyze(f),
			(client, f) => client.AnalyzeAsync(f),
			(client, r) => client.Analyze(r),
			(client, r) => client.AnalyzeAsync(r)
		);
	}
}
