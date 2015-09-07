using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tests.Framework.MockResponses;

namespace Tests.Framework
{
	public class VirtualClusterConnection : InMemoryConnection
	{
		private VirtualCluster _cluster;

		public VirtualClusterConnection(VirtualCluster cluster)
		{
			this._cluster = cluster;
		}

		public bool IsSniffRequest(RequestData requestData) =>
			requestData.Path.StartsWith("_nodes/_all/settings", StringComparison.Ordinal);

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
							return this.ReturnConnectionStatus<TReturn>(requestData, SniffResponse.Create(this._cluster.Nodes));
						}
						throw new ElasticsearchException(PipelineFailure.BadResponse, (Exception)null);
					}
				}
				foreach (var sniffRule in this._cluster.SniffingRules.Where(s => !s.OnPort.HasValue))
				{
					if (sniffRule.AllCalls.GetValueOrDefault(false))
					{
						if (sniffRule.Succeeds)
						{
							this._cluster = sniffRule.NewClusterState ?? this._cluster;
							return this.ReturnConnectionStatus<TReturn>(requestData, SniffResponse.Create(this._cluster.Nodes));
						}
						throw new ElasticsearchException(PipelineFailure.BadResponse, (Exception)null);
					}
				}
				return this.ReturnConnectionStatus<TReturn>(requestData, SniffResponse.Create(this._cluster.Nodes));
			}
			return this.ReturnConnectionStatus<TReturn>(requestData, CallResponse());
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