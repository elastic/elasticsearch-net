using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.Scripting.GetScript
{
	public class GetScriptApiTests
		: ApiTestBase<ReadOnlyCluster, IGetScriptResponse, IGetScriptRequest, GetScriptDescriptor, GetScriptRequest>
	{
		public GetScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "scrpt1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetScript(_name, f),
			fluentAsync: (client, f) => client.GetScriptAsync(_name, f),
			request: (client, r) => client.GetScript(r),
			requestAsync: (client, r) => client.GetScriptAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_scripts/{_name}";

		protected override GetScriptDescriptor NewDescriptor() => new GetScriptDescriptor(_name);

		protected override Func<GetScriptDescriptor, IGetScriptRequest> Fluent => d => d;

		protected override GetScriptRequest Initializer => new GetScriptRequest(_name);
	}
}
