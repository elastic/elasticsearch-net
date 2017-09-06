using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.Scripting.DeleteScript
{
	public class DeleteScriptApiTests
		: ApiTestBase<ReadOnlyCluster, IDeleteScriptResponse, IDeleteScriptRequest, DeleteScriptDescriptor, DeleteScriptRequest>
	{
		public DeleteScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "scrpt1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteScript(_name, f),
			fluentAsync: (client, f) => client.DeleteScriptAsync(_name, f),
			request: (client, r) => client.DeleteScript(r),
			requestAsync: (client, r) => client.DeleteScriptAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_scripts/{_name}";

		protected override DeleteScriptDescriptor NewDescriptor() => new DeleteScriptDescriptor(_name);

		protected override Func<DeleteScriptDescriptor, IDeleteScriptRequest> Fluent => d => d;

		protected override DeleteScriptRequest Initializer => new DeleteScriptRequest(_name);
	}
}
