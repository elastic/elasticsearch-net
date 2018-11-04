using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasApiRemoveIndexTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IBulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasApiRemoveIndexTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			actions = new object[]
			{
				new Dictionary<string, object> { { "add", new { alias = CallIsolatedValue + "-1", index = CallIsolatedValue + "-2" } } },
				new Dictionary<string, object> { { "remove_index", new { index = CallIsolatedValue + "-1" } } },
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.Add(a => a.Alias(CallIsolatedValue + "-1").Index(CallIsolatedValue + "-2"))
			.RemoveIndex(a => a.Index(CallIsolatedValue + "-1"));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasAddAction { Add = new AliasAddOperation { Alias = CallIsolatedValue + "-1", Index = CallIsolatedValue + "-2" } },
				new AliasRemoveIndexAction { RemoveIndex = new AliasRemoveIndexOperation { Index = Infer.Index(CallIsolatedValue + "-1") } },
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_aliases";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values.Values)
			{
				var createIndexResponse = client.CreateIndex(value + "-1", c => c);
				if (!createIndexResponse.IsValid)
					throw new Exception(createIndexResponse.DebugInformation);

				createIndexResponse = client.CreateIndex(value + "-2", c => c);
				if (!createIndexResponse.IsValid)
					throw new Exception(createIndexResponse.DebugInformation);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Alias(f),
			(client, f) => client.AliasAsync(f),
			(client, r) => client.Alias(r),
			(client, r) => client.AliasAsync(r)
		);
	}
}
