using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IBulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			actions = new object[]
			{
				new Dictionary<string, object>
					{ { "add", new { alias = "alias", index = CallIsolatedValue, index_routing = "x", search_routing = "y" } } },
				new Dictionary<string, object> { { "remove", new { alias = "alias", index = CallIsolatedValue } } },
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.Add(a => a.Alias("alias").Index(CallIsolatedValue).IndexRouting("x").SearchRouting("y"))
			.Remove(a => a.Alias("alias").Index(CallIsolatedValue));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasAddAction
					{ Add = new AliasAddOperation { Alias = "alias", Index = CallIsolatedValue, IndexRouting = "x", SearchRouting = "y" } },
				new AliasRemoveAction { Remove = new AliasRemoveOperation { Alias = "alias", Index = Infer.Index(CallIsolatedValue) } },
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_aliases";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Alias(f),
			(client, f) => client.AliasAsync(f),
			(client, r) => client.Alias(r),
			(client, r) => client.AliasAsync(r)
		);
	}
}
