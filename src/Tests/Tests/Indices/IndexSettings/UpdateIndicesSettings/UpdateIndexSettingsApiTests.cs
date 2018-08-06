using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Indices.IndexSettings.UpdateIndicesSettings
{
	public class UpdateIndexSettingsApiTests : ApiIntegrationTestBase<WritableCluster, IUpdateIndexSettingsResponse, IUpdateIndexSettingsRequest, UpdateIndexSettingsDescriptor, UpdateIndexSettingsRequest>
	{
		public UpdateIndexSettingsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var index = value.Value;
				var createIndexResponse = client.CreateIndex(index);

				if (!createIndexResponse.IsValid)
					throw new Exception($"Invalid response when setting up index for integration test {this.GetType().Name}");
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.UpdateIndexSettings(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.UpdateIndexSettingsAsync(CallIsolatedValue, f),
			request: (client, r) => client.UpdateIndexSettings(r),
			requestAsync: (client, r) => client.UpdateIndexSettingsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"{CallIsolatedValue}/_settings";

		protected override object ExpectJson { get; } = new Dictionary<string, object>
		{
			{ "index.blocks.write", false },
			{ "index.number_of_replicas", 2 },
			{ "index.priority", 2 }
		};

		protected override Func<UpdateIndexSettingsDescriptor, IUpdateIndexSettingsRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.IndexSettings(i => i
				.BlocksWrite(false)
				.NumberOfReplicas(2)
				.Priority(2)
			);

		protected override UpdateIndexSettingsRequest Initializer => new UpdateIndexSettingsRequest(CallIsolatedValue)
		{
			IndexSettings = new Nest.IndexSettings(new Dictionary<string, object>
			{
				{ "index.number_of_replicas", 3 },
				{ "index.priority", 2 }
			})
			{
				BlocksWrite = false,
				NumberOfReplicas = 2, //this should win from the value provided in the base dictionary
			}
		};
	}
}
