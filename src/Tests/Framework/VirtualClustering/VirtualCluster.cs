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

		public VirtualCluster Sniff(Func<SniffRule, ISniffRule> selector)
		{
			this.SniffingRules.Add(selector(new SniffRule()));
			return this;
		}

		public SealedVirtualCluster StaticConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			//TODO inject DateTimeProver
			var nodes = seedNodesSelector?.Invoke(this._nodes) ?? this._nodes;
			return new SealedVirtualCluster(this, new StaticConnectionPool(nodes, randomize: false));
		}

		public SealedVirtualCluster SniffingConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			//TODO inject DateTimeProver
			var nodes = seedNodesSelector?.Invoke(this._nodes) ?? this._nodes;
			return new SealedVirtualCluster(this, new SniffingConnectionPool(nodes, randomize: false));
		}
	}

	public class SniffRule : ISniffRule
	{
		private ISniffRule Self => this;
		int? ISniffRule.OnPort { get; set; }
		bool ISniffRule.Succeeds { get; set; }
		bool? ISniffRule.AllCalls { get; set; }
		int? ISniffRule.NthCall { get; set; }
		VirtualCluster ISniffRule.NewClusterState { get; set; }

		public SniffRule OnPort(int port)
		{
			Self.OnPort = port;
			return this;
		}
		public SniffRule FailsOn(int call)
		{
			Self.NthCall = call;
			Self.Succeeds = false;
			return this;
		}

		public SniffRule SucceedsOn(int call, VirtualCluster cluster = null)
		{
			Self.NthCall = call;
			Self.Succeeds = true;
			Self.NewClusterState = cluster;
			return this;
		}

		public SniffRule FailAlways()
		{
			Self.AllCalls = true;
			Self.Succeeds = false;
			return this;
		}

		public SniffRule SucceedAlways(VirtualCluster cluster = null)
		{
			Self.AllCalls = true;
			Self.Succeeds = true;
			Self.NewClusterState = cluster;
			return this;
		}

	}
}