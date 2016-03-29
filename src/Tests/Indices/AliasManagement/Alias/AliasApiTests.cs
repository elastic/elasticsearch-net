using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Indices.AliasManagement.Alias
{
	[Collection(IntegrationContext.Indexing)]
	public class AliasApiTests : ApiIntegrationTestBase<IBulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var index in values.Values)
				client.CreateIndex(index);
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Alias(f),
			fluentAsync: (client, f) => client.AliasAsync(f),
			request: (client, r) => client.Alias(r),
			requestAsync: (client, r) => client.AliasAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_aliases";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new 
		{
			actions = new object[]
			{
				new Dictionary<string, object> { { "add", new { alias = "alias", index = CallIsolatedValue, index_routing="x", search_routing="y"} } }, 
				new Dictionary<string, object> { { "remove", new { alias = "alias", index = CallIsolatedValue} } }, 
			}
		};

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.Add(a=>a.Alias("alias").Index(CallIsolatedValue).IndexRouting("x").SearchRouting("y"))
			.Remove(a=>a.Alias("alias").Index(CallIsolatedValue))
		;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasAddAction { Add = new AliasAddOperation {Alias = "alias", Index = CallIsolatedValue, IndexRouting = "x", SearchRouting = "y"} },
				new AliasRemoveAction {Remove = new AliasRemoveOperation {Alias = "alias", Index = Infer.Index(CallIsolatedValue) }},
			}
		};
	}
}
