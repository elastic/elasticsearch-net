using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.Scripting.DeleteScript
{
	public class DeleteScriptApiTests
		: ApiTestBase<ReadOnlyCluster, IDeleteScriptResponse, IDeleteScriptRequest, DeleteScriptDescriptor, DeleteScriptRequest>
	{
		private static readonly string _name = "scrpt1";

		public DeleteScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteScriptDescriptor, IDeleteScriptRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteScriptRequest Initializer => new DeleteScriptRequest(_name);
		protected override string UrlPath => $"/_scripts/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteScript(_name, f),
			(client, f) => client.DeleteScriptAsync(_name, f),
			(client, r) => client.DeleteScript(r),
			(client, r) => client.DeleteScriptAsync(r)
		);

		protected override DeleteScriptDescriptor NewDescriptor() => new DeleteScriptDescriptor(_name);
	}
}
