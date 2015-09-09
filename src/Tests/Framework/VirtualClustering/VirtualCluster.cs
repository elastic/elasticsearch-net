using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Framework
{
	public class VirtualCluster
	{
		private readonly List<Node> _nodes;
		private IConnectionPool _connectionPool;

		public List<ISniffRule> SniffingRules { get; } = new List<ISniffRule>();
		public List<IRule> PingingRules { get; } = new List<IRule>();
		public List<IClientCallRule> ClientCallRules { get; } = new List<IClientCallRule>();
		public TestableDateTimeProvider DateTimeProvider { get; } = new TestableDateTimeProvider();

		public IReadOnlyList<Node> Nodes => _nodes;

		public VirtualCluster(IEnumerable<Node> nodes)
		{
			this._nodes = nodes.ToList();
		}

		public VirtualCluster MasterEligable(params int[] ports)
		{
			foreach (var node in this._nodes.Where(n => !ports.Contains(n.Uri.Port)))
				node.MasterEligable = false;
			return this;
		}

		public VirtualCluster StoresNoData(params int[] ports)
		{
			foreach (var node in this._nodes.Where(n => ports.Contains(n.Uri.Port)))
				node.HoldsData = false;
			return this;
		}

		public VirtualCluster Ping(Func<PingRule, IRule> selector)
		{
			this.PingingRules.Add(selector(new PingRule()));
			return this;
		}

		public VirtualCluster Sniff(Func<SniffRule, ISniffRule> selector)
		{
			this.SniffingRules.Add(selector(new SniffRule()));
			return this;
		}
		public VirtualCluster ClientCalls(Func<ClientCallRule, IClientCallRule> selector)
		{
			this.ClientCallRules.Add(selector(new ClientCallRule()));
			return this;
		}

		public SealedVirtualCluster SingleNodeConnection(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(this._nodes) ?? this._nodes;
			return new SealedVirtualCluster(this, new SingleNodeConnectionPool(nodes.First().Uri), this.DateTimeProvider);
		}
		public SealedVirtualCluster StaticConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(this._nodes) ?? this._nodes;
			return new SealedVirtualCluster(this, new StaticConnectionPool(nodes, randomize: false, dateTimeProvider: this.DateTimeProvider), this.DateTimeProvider);
		}

		public SealedVirtualCluster SniffingConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(this._nodes) ?? this._nodes;
			return new SealedVirtualCluster(this, new SniffingConnectionPool(nodes, randomize: false, dateTimeProvider: this.DateTimeProvider), this.DateTimeProvider);
		}
	}

}