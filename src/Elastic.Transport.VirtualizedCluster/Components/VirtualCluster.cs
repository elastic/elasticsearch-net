// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport.VirtualizedCluster.Products;
using Elastic.Transport.VirtualizedCluster.Providers;
using Elastic.Transport.VirtualizedCluster.Rules;

namespace Elastic.Transport.VirtualizedCluster.Components
{
	public class VirtualCluster
	{
		protected VirtualCluster(IEnumerable<Node> nodes, IMockProductRegistration productRegistration)
		{
			ProductRegistration = productRegistration;
			InternalNodes = nodes.ToList();
		}

		public List<IClientCallRule> ClientCallRules { get; } = new List<IClientCallRule>();
		public TestableDateTimeProvider DateTimeProvider { get; } = new TestableDateTimeProvider();

		protected List<Node> InternalNodes { get; }
		public IReadOnlyList<Node> Nodes => InternalNodes;
		public List<IRule> PingingRules { get; } = new List<IRule>();

		public List<ISniffRule> SniffingRules { get; } = new List<ISniffRule>();
		internal string PublishAddressOverride { get; private set; }

		internal bool SniffShouldReturnFqnd { get; private set; }
		internal string ElasticsearchVersion { get; private set; } = "7.0.0";

		public IMockProductRegistration ProductRegistration { get; }

		public VirtualCluster SniffShouldReturnFqdn()
		{
			SniffShouldReturnFqnd = true;
			return this;
		}

		public VirtualCluster SniffElasticsearchVersionNumber(string version)
		{
			ElasticsearchVersion = version;
			return this;
		}

		public VirtualCluster PublishAddress(string publishHost)
		{
			PublishAddressOverride = publishHost;
			return this;
		}

		public VirtualCluster Ping(Func<PingRule, IRule> selector)
		{
			PingingRules.Add(selector(new PingRule()));
			return this;
		}

		public VirtualCluster Sniff(Func<SniffRule, ISniffRule> selector)
		{
			SniffingRules.Add(selector(new SniffRule()));
			return this;
		}

		public VirtualCluster ClientCalls(Func<ClientCallRule, IClientCallRule> selector)
		{
			ClientCallRules.Add(selector(new ClientCallRule()));
			return this;
		}

		public SealedVirtualCluster SingleNodeConnection(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(InternalNodes) ?? InternalNodes;
			return new SealedVirtualCluster(this, new SingleNodeConnectionPool(nodes.First().Uri), DateTimeProvider, ProductRegistration);
		}

		public SealedVirtualCluster StaticConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(InternalNodes) ?? InternalNodes;
			return new SealedVirtualCluster(this, new StaticConnectionPool(nodes, false, DateTimeProvider), DateTimeProvider, ProductRegistration);
		}

		public SealedVirtualCluster SniffingConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(InternalNodes) ?? InternalNodes;
			return new SealedVirtualCluster(this, new SniffingConnectionPool(nodes, false, DateTimeProvider), DateTimeProvider, ProductRegistration);
		}

		public SealedVirtualCluster StickyConnectionPool(Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null)
		{
			var nodes = seedNodesSelector?.Invoke(InternalNodes) ?? InternalNodes;
			return new SealedVirtualCluster(this, new StickyConnectionPool(nodes, DateTimeProvider), DateTimeProvider, ProductRegistration);
		}

		public SealedVirtualCluster StickySniffingConnectionPool(Func<Node, float> sorter = null,
			Func<IList<Node>, IEnumerable<Node>> seedNodesSelector = null
		)
		{
			var nodes = seedNodesSelector?.Invoke(InternalNodes) ?? InternalNodes;
			return new SealedVirtualCluster(this, new StickySniffingConnectionPool(nodes, sorter, DateTimeProvider), DateTimeProvider, ProductRegistration);
		}
	}
}
