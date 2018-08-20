using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.Scripting.ExecutePainlessScript
{
	[SkipVersion("<6.3.0", "this API was introduced in 6.3.0")]
	public class ExecutePainlessScriptApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IExecutePainlessScriptResponse<string>, IExecutePainlessScriptRequest, ExecutePainlessScriptDescriptor, ExecutePainlessScriptRequest>
	{
		public ExecutePainlessScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _painlessScript = "params.count / params.total";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ExecutePainlessScript<string>(f),
			fluentAsync: (client, f) => client.ExecutePainlessScriptAsync<string>(f),
			request: (client, r) => client.ExecutePainlessScript<string>(r),
			requestAsync: (client, r) => client.ExecutePainlessScriptAsync<string>(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_scripts/painless/_execute";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			script = new
			{
				source = _painlessScript,
				@params = new { count = 100.0, total = 1000.0 }
			},
		};

		protected override Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> Fluent => d => d
			.Script(s=>s
				.Source(_painlessScript)
				.Params(p => p.Add("count", 100.0).Add("total", 1000.0))
			);

		protected override ExecutePainlessScriptRequest Initializer => new ExecutePainlessScriptRequest
		{
			Script = new InlineScript(_painlessScript)
			{
				Params = new Dictionary<string, object>
				{
					{ "count", 100.0 },
					{ "total", 1000.0 },
				}
			}
		};

		protected override void ExpectResponse(IExecutePainlessScriptResponse<string> response)
		{
			response.ShouldBeValid();
			response.Result.Should().NotBeNullOrWhiteSpace();
		}
	}
}
