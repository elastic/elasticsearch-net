using System;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net.Serialization;
using System.IO;

namespace Tests.Framework
{
	public class SealedVirtualCluster
	{
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;
		private readonly IConnection _connection;

		public SealedVirtualCluster(VirtualCluster cluster, IConnectionPool pool)
		{
			this._cluster = cluster;
			this._connectionPool = pool;
			this._connection = new VirtualClusterConnection(cluster);
		}

		private ConnectionSettings CreateSettings() =>
			new ConnectionSettings(this._connectionPool, this._connection);

		public VirtualizedCluster AllDefaults() =>
			new VirtualizedCluster(this._cluster, this._connectionPool, CreateSettings());

		public VirtualizedCluster Settings(Func<ConnectionSettings, ConnectionSettings> selector) =>
			new VirtualizedCluster(this._cluster, this._connectionPool, selector(CreateSettings()));
	}



	public class VirtualClusterConnection : InMemoryConnection
	{
		private VirtualCluster _cluster;

		public VirtualClusterConnection(VirtualCluster cluster)
		{
			this._cluster = cluster;
		}

		public bool IsSniffRequest(RequestData requestData) =>
			requestData.Path.StartsWith("_nodes/_all/clear", StringComparison.Ordinal);

		public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData)
		{
			if (IsSniffRequest(requestData))
			{
				foreach (var sniffRule in this._cluster.SniffingRules.Where(s => s.OnPort.HasValue))
                {
					if (sniffRule.OnPort.Value == requestData.Uri.Port)
					{
						//if (sniffRule.NthCall)
						if (sniffRule.Succeeds)
						{
							this._cluster = sniffRule.NewClusterState ?? this._cluster;
							return this.ReturnConnectionStatus<TReturn>(requestData, SniffResponse());
						}
						throw new ElasticsearchException(PipelineFailure.BadResponse, (Exception)null);
					}
				}
				foreach (var sniffRule in this._cluster.SniffingRules.Where(s=>!s.OnPort.HasValue))
				{
					if (sniffRule.AllCalls.GetValueOrDefault(false))
					{
						if (sniffRule.Succeeds)
						{
							this._cluster = sniffRule.NewClusterState ?? this._cluster;
							return this.ReturnConnectionStatus<TReturn>(requestData, SniffResponse());
						}
						throw new ElasticsearchException(PipelineFailure.BadResponse, (Exception)null);
					}
				}
				return this.ReturnConnectionStatus<TReturn>(requestData, SniffResponse());
			}
			return this.ReturnConnectionStatus<TReturn>(requestData, CallResponse());
		}

		private IDictionary<string, object> SniffResponseNodes() =>
			this._cluster.Nodes.ToDictionary(kv => kv.Id ?? Guid.NewGuid().ToString("N").Substring(0, 8), kv => (object)new
			{
				name = kv.Name ?? Guid.NewGuid().ToString("N").Substring(0, 8),
				transport_address = $"inet[/127.0.0.1:{kv.Uri.Port}]",
				http_address = $"inet[/127.0.0.1:{kv.Uri.Port}]",
				host = Guid.NewGuid().ToString("N").Substring(0, 8),
				ip = "127.0.0.1",
				version = TestClient.ElasticsearchVersion,
				build = Guid.NewGuid().ToString("N").Substring(0, 8),
			});

		private byte[] SniffResponse()
		{
			var response = new
			{
				cluster_name = "elasticsearch-test-cluster",
				nodes = this.SniffResponseNodes()
			};
			using (var ms = new MemoryStream())
			{
				new ElasticsearchDefaultSerializer().Serialize(response, ms);
				return ms.ToArray();
			}
		}

		private byte[] CallResponse()
		{
			var response = new
			{
				name = "Razor Fist",
				cluster_name = "elasticsearch-test-cluster",
				version = new
				{
					number = "2.0.0",
					build_hash = "af1dc6d8099487755c3143c931665b709de3c764",
					build_timestamp = "2015-07-07T11:28:47Z",
					build_snapshot = true,
					lucene_version = "5.2.1"
				},
				tagline = "You Know, for Search"
			};

			using (var ms = new MemoryStream())
			{
				new ElasticsearchDefaultSerializer().Serialize(response, ms);
				return ms.ToArray();
			}
		}

		public override Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData)
		{
			return Task.FromResult(this.Request<TReturn>(requestData));
		}
	}

}