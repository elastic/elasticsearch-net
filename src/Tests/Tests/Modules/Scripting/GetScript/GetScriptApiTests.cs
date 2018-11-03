using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.Scripting.GetScript
{
	public class GetScriptApiTests
		: ApiTestBase<ReadOnlyCluster, IGetScriptResponse, IGetScriptRequest, GetScriptDescriptor, GetScriptRequest>
	{
		private static readonly string _name = "scrpt1";

		public GetScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetScriptDescriptor, IGetScriptRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetScriptRequest Initializer => new GetScriptRequest(_name);
		protected override string UrlPath => $"/_scripts/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.GetScript(_name, f),
			(client, f) => client.GetScriptAsync(_name, f),
			(client, r) => client.GetScript(r),
			(client, r) => client.GetScriptAsync(r)
		);

		protected override GetScriptDescriptor NewDescriptor() => new GetScriptDescriptor(_name);
	}
}
