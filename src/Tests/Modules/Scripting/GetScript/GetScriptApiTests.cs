using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.Scripting.GetScript
{
	[Collection(IntegrationContext.ReadOnly)]
	public class GetScriptApiTests : ApiTestBase<IGetScriptResponse, IGetScriptRequest, GetScriptDescriptor, GetScriptRequest>
	{
		public GetScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _language = "groovy";
		private static readonly string _name = "scrpt1";


		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.GetScript(_language, _name, f),
			fluentAsync: (client, f) => client.GetScriptAsync(_language, _name, f),
			request: (client, r) => client.GetScript(r),
			requestAsync: (client, r) => client.GetScriptAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_scripts/{_language}/{_name}";

		protected override GetScriptDescriptor NewDescriptor() => new GetScriptDescriptor(_language, _name);

		protected override Func<GetScriptDescriptor, IGetScriptRequest> Fluent => d => d;

		protected override GetScriptRequest Initializer => new GetScriptRequest(_language, _name);
	}
}
