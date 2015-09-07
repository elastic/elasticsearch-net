using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tests.Framework.MockResponses;
using System.Threading;

namespace Tests.Framework
{
	public class VirtualClusterConnection : InMemoryConnection
	{
		private VirtualCluster _cluster;

		public VirtualClusterConnection(VirtualCluster cluster)
		{
			this._cluster = cluster;
		}

		private int _sniffed = 0;
		public bool IsSniffRequest(RequestData requestData) => requestData.Path.StartsWith("_nodes/_all/settings", StringComparison.Ordinal);
		private int _pinged = 0;
		public bool IsPingRequest(RequestData requestData) => requestData.Path == "/" && requestData.Method == HttpMethod.HEAD;

		public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData)
		{
			if (IsSniffRequest(requestData))
			{
				var sniffed = Interlocked.Increment(ref _sniffed);
				return HandleRules<TReturn, ISniffRule>(
					requestData, 
					this._cluster.SniffingRules, 
					(r) => this._cluster = r.NewClusterState ?? this._cluster, 
					()=>SniffResponse.Create(this._cluster.Nodes)
				);
			}
			if (IsPingRequest(requestData))
			{
				var pinged = Interlocked.Increment(ref _pinged);
				return HandleRules<TReturn, IRule>(
					requestData, 
					this._cluster.PingingRules, 
					(r) => { }, 
					()=> CallResponse()
				);
			}
			return this.ReturnConnectionStatus<TReturn>(requestData, CallResponse());
		}

		private ElasticsearchResponse<TReturn> HandleRules<TReturn, TRule>(
			RequestData requestData, IEnumerable<TRule> rules,
			Action<TRule> beforeReturn,
			Func<byte[]> successResponse
			) where TReturn : class where TRule:  IRule
		{
			foreach (var rule in rules.Where(s => s.OnPort.HasValue))
			{
				if (rule.OnPort.Value == requestData.Uri.Port)
				{
					if (rule.Succeeds)
					{
						beforeReturn?.Invoke(rule);
						return this.ReturnConnectionStatus<TReturn>(requestData, successResponse());
					}
					throw new ElasticsearchException(PipelineFailure.BadResponse, (Exception)null);
				}
			}
			foreach (var rule in rules.Where(s => !s.OnPort.HasValue))
			{
				if (!rule.AllCalls.GetValueOrDefault(false)) continue;
				if (rule.Succeeds)
				{
					beforeReturn?.Invoke(rule);
					return this.ReturnConnectionStatus<TReturn>(requestData,successResponse());
				}
				throw new ElasticsearchException(PipelineFailure.BadResponse, (Exception)null);
			}
			return this.ReturnConnectionStatus<TReturn>(requestData,successResponse());
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