using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Indices.AliasManagement.Alias
{
	public class AliasApiRemoveIndexTests : ApiIntegrationAgainstNewIndexTestBase<WritableCluster, IBulkAliasResponse, IBulkAliasRequest, BulkAliasDescriptor, BulkAliasRequest>
	{
		public AliasApiRemoveIndexTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
				new Dictionary<string, object> { { "remove_index", new { index = CallIsolatedValue + "-1"} } },
				new Dictionary<string, object> { { "add", new { alias = CallIsolatedValue + "-1", index = CallIsolatedValue + "-2" } } },
			}
		};

		protected override Func<BulkAliasDescriptor, IBulkAliasRequest> Fluent => d => d
			.RemoveIndex(a => a.Index(CallIsolatedValue + "-1"))
			.Add(a=>a.Alias(CallIsolatedValue + "-1").Index(CallIsolatedValue + "-2"))
		;

		protected override BulkAliasRequest Initializer => new BulkAliasRequest
		{
			Actions = new List<IAliasAction>
			{
				new AliasRemoveIndexAction { RemoveIndex = new AliasRemoveIndexOperation { Index = Infer.Index(CallIsolatedValue + "-1") } },
				new AliasAddAction { Add = new AliasAddOperation {Alias = CallIsolatedValue + "-1", Index = CallIsolatedValue + "-2"} },
			}
		};
	}
}
