using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.Analyze
{
	[Collection(IntegrationContext.ReadOnly)]
	public class AnalyzeApiTests 
		: ApiIntegrationTestBase<IAnalyzeResponse, IAnalyzeRequest, AnalyzeDescriptor, AnalyzeRequest>
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

		protected override Func<AnalyzeDescriptor, IAnalyzeRequest> Fluent => d => d
			.Text("hello world", "domination");

		protected override AnalyzeRequest Initializer => new AnalyzeRequest
		{
			Text = new [] { "hello world", "domination" }
		};
	}
}