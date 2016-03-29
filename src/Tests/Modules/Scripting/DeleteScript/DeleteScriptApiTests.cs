using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.Scripting.DeleteScript
{
	[Collection(IntegrationContext.ReadOnly)]
	public class DeleteScriptApiTests : ApiTestBase<IDeleteScriptResponse, IDeleteScriptRequest, DeleteScriptDescriptor, DeleteScriptRequest>
	{
		public DeleteScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _language = "groovy";
		private static readonly string _name = "scrpt1";


		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteScript(_language, _name, f),
			fluentAsync: (client, f) => client.DeleteScriptAsync(_language, _name, f),
			request: (client, r) => client.DeleteScript(r),
			requestAsync: (client, r) => client.DeleteScriptAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_scripts/{_language}/{_name}";

		protected override DeleteScriptDescriptor NewDescriptor() => new DeleteScriptDescriptor(_language, _name);

		protected override Func<DeleteScriptDescriptor, IDeleteScriptRequest> Fluent => d => d;

		protected override DeleteScriptRequest Initializer => new DeleteScriptRequest(_language, _name);
	}
}
