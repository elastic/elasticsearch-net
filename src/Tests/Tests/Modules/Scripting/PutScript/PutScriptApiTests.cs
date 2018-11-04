using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.Scripting.PutScript
{
	public class PutScriptApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IPutScriptResponse, IPutScriptRequest, PutScriptDescriptor, PutScriptRequest>
	{
		private static readonly string _name = "scrpt1";

		public PutScriptApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			script = new
			{
				lang = "painless",
				source = "1+1"
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutScriptDescriptor, IPutScriptRequest> Fluent => d => d
			.Painless("1+1");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutScriptRequest Initializer => new PutScriptRequest(_name)
		{
			Script = new PainlessScript("1+1")
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_scripts/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutScript(_name, f),
			(client, f) => client.PutScriptAsync(_name, f),
			(client, r) => client.PutScript(r),
			(client, r) => client.PutScriptAsync(r)
		);

		protected override PutScriptDescriptor NewDescriptor() => new PutScriptDescriptor(_name);

		protected override void ExpectResponse(IPutScriptResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
