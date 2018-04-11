using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elastic.Managed.Ephemeral;
using Elasticsearch.Net;
using Nest;

namespace Tests.Framework.ManagedElasticsearch
{
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
}