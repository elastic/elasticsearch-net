using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.Scripting.PutScript
{
	public class PutScriptApiTests
		: ApiTestBase<ReadOnlyCluster, IPutScriptResponse, IPutScriptRequest, PutScriptDescriptor, PutScriptRequest>
	{
		private static readonly string _language = "groovy";
		private static readonly string _name = "scrpt1";

		public PutScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			script = "1+1"
		};

		protected override Func<PutScriptDescriptor, IPutScriptRequest> Fluent => d => d
			.Script("1+1");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutScriptRequest Initializer => new PutScriptRequest(_language, _name)
		{
			Script = "1+1"
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_scripts/{_language}/{_name}";


		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutScript(_language, _name, f),
			(client, f) => client.PutScriptAsync(_language, _name, f),
			(client, r) => client.PutScript(r),
			(client, r) => client.PutScriptAsync(r)
		);

		protected override PutScriptDescriptor NewDescriptor() => new PutScriptDescriptor(_language, _name);
	}
}
