using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit;
using Elastic.Xunit.Sdk;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

[assembly: Xunit.TestFrameworkAttribute("Elastic.Xunit.Sdk.ElasticTestFramework", "Elastic.Xunit")]
[assembly: ElasticXunitConfiguration(typeof(NestXunitRunOptions))]

namespace Tests.Framework.ManagedElasticsearch
{
	/// <summary> Feeding TestClient.Configuration options to the runner</summary>
	public class NestXunitRunOptions : ElasticXunitRunOptions
	{
		public NestXunitRunOptions()
		{
			this.RunIntegrationTests = TestClient.Configuration.RunIntegrationTests;
			this.RunUnitTests = TestClient.Configuration.RunUnitTests;
			this.ClusterFilter = TestClient.Configuration.ClusterFilter;
			this.TestFilter = TestClient.Configuration.TestFilter;
		}
	}

	internal static class EphemeralClusterExtensions
	{
		private static readonly ConcurrentDictionary<IEphemeralCluster, IElasticClient> Clients = new ConcurrentDictionary<IEphemeralCluster, IElasticClient>();

		public static bool RunningFiddler => Process.GetProcessesByName("fiddler").Any();

		public static IElasticClient GetOrAddClient(
			this IEphemeralCluster cluster,
			Func<ConnectionSettings, ConnectionSettings> createSettings = null,
			Func<ICollection<Uri>, IConnectionPool> createPool = null)
		{
			return Clients.GetOrAdd(cluster, (c) =>
			{
				var host = (RunningFiddler) ? "ipv4.fiddler" : "localhost";
				createSettings = createSettings ?? (s => s);
				createPool = createPool ?? (uris => new StaticConnectionPool(uris));
				var connectionPool = createPool(c.NodesUris(host));
				var connection = TestClient.Configuration.RunIntegrationTests
					? TestClient.CreateLiveConnection()
					: new InMemoryConnection();
				var settings = TestClient.CreateSettings(createSettings, connection, connectionPool);
				var client = new ElasticClient(settings);
				return client;
			});

		}
	}

	public abstract class ClusterTestClassBase<TCluster> : IClusterFixture<TCluster>
		where TCluster : IEphemeralCluster<EphemeralClusterConfiguration>, INestTestCluster, new()
	{
		public TCluster Cluster { get; }
		public IElasticClient Client => this.Cluster.Client;

		protected ClusterTestClassBase(TCluster cluster)
		{
			this.Cluster = cluster;
		}
	}

}
