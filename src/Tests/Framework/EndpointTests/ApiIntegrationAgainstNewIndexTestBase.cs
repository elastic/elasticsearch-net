using System.Linq;
using Elastic.Managed.Ephemeral;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class ApiIntegrationAgainstNewIndexTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		: ApiIntegrationTestBase<TCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected ApiIntegrationAgainstNewIndexTestBase(TCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values) client.CreateIndex(index, this.CreateIndexSettings).ShouldBeValid();
			var indices = Infer.Indices(values.Values.Select(i => (IndexName)i));
			client.ClusterHealth(f => f.WaitForStatus(WaitForStatus.Yellow).Index(indices))
				.ShouldBeValid();
		}

		protected virtual ICreateIndexRequest CreateIndexSettings(CreateIndexDescriptor create) => create;
	}
}
